using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.Helpers.Hashing;
using OpenArtspaceGallery.Infrastructure.FileStorage;
using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Settings;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class FilesService : IFilesService
{
    private readonly IFilesDao _filesDao;
    private readonly FilesStorageSettings _filesStorageSettings;
    private readonly IResizeService _resizeService;
    
    public FilesService
    (
        IOptions<FilesStorageSettings> filesStorageSettings,
        IFilesDao filesDao,
        IResizeService resizeService
    )
    {
        _filesStorageSettings = filesStorageSettings.Value;
        _filesDao = filesDao;
        _resizeService = resizeService;
    }
    
    public async Task<FileInfoDto> UploadFileAsync(IFormFile file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null!");

        #region Storage file name

        var fileId = Guid.NewGuid();
        
        #endregion
        
        var filePath = FileStorageHelper.GetFilePath(_filesStorageSettings.RootPath, fileId);
        var content = new byte[file.Length];

        await using (var fileStream = file.OpenReadStream())
        {
            await fileStream.ReadExactlyAsync(content, 0, (int)file.Length);
        }

        var fileTypeId = await _filesDao.GetFileTypeIdByMimeTypeAsync(file.ContentType);
        if (fileTypeId == null)
        {
            throw new ArgumentException($"Unsupported file type: {file.ContentType}");
        }

        var fileToSave = new FileToSaveDto
        {
            Id = fileId,
            OriginalFileName = file.FileName,
            FileTypeId = fileTypeId.Value,
            FilePath = filePath,
            Content = content
        };
        
        var fileDbo = await SaveFileAsync(fileToSave);
        
        var previewFile = await _resizeService.ResizeImageAsync(content, _filesStorageSettings.MaxSize);
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var resizedPhotoName = Path.GetFileNameWithoutExtension(file.FileName)
                               + "_preview"
                               + Path.GetExtension(file.FileName);
        var previewPath = Path.Combine(desktopPath, resizedPhotoName);

        await File.WriteAllBytesAsync(previewPath, previewFile);
        
        return new FileInfoDto(fileDbo.Id, fileDbo.OriginalName);
    }
    
    public async Task<FileDbo> SaveFileAsync(FileToSaveDto dto)
    {
        var storagePath = Path.GetRelativePath(_filesStorageSettings.RootPath, dto.FilePath);
        
        var fileDbo = new FileDbo
        {
            Id = dto.Id,
            Type = new FileTypeDbo { Id = dto.FileTypeId },
            OriginalName = dto.OriginalFileName,
            StoragePath = storagePath,
            Hash = SHA512Helper.CalculateSHA512(dto.Content)
        };
        
        return await _filesDao.CreateFileAsync(fileDbo);
    }

    public async Task<FileForDownload> GetFileForDownloadAsync(Guid fileId)
    {
        var metadata = await _filesDao.GetFileMetadataAsync(fileId);

        var fileBytes = await File.ReadAllBytesAsync(Path.Combine(_filesStorageSettings.RootPath, metadata.StoragePath));

        return new FileForDownload
        {
            Content = fileBytes,
            OriginalName = metadata.OriginalName,
            Type = metadata.Type,
            Hash = metadata.Hash,
            LastModificationTime = metadata.LastModificationTime
        };
    }
}