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
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Implementation;

public class FilesService : IFilesService
{
    private readonly IFilesDao _filesDao;
    private readonly FilesStorageSettings _filesStorageSettings;
    
    public FilesService
    (
        IOptions<FilesStorageSettings> filesStorageSettings,
        IFilesDao filesDao,
        IResizeService resizeService
    )
    {
        _filesStorageSettings = filesStorageSettings.Value;
        _filesDao = filesDao;
    }
    
    public async Task<FileInfo> UploadFileAsync(IFormFile file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null!");
        
        var content = new byte[file.Length];

        await using (var fileStream = file.OpenReadStream())
        {
            await fileStream.ReadExactlyAsync(content, 0, (int)file.Length);
        }

        var originalFileInfo = await SaveFileAsync(file.FileName, file.ContentType, content);
        
        // Generating preview
        //var previewFileContent = await _resizeService.ResizeImageAsync(content, _filesStorageSettings.MaxSize);
        //await SaveFileAsync($"{ file.FileName }_preview", file.ContentType, previewFileContent);
        
        return originalFileInfo;
    }

    public async Task<FileInfo> SaveFileAsync
    (
        string name,
        string type,
        byte[] content
    )
    {
        var id = Guid.NewGuid();
        var path = FileStorageHelper.GetFilePath(_filesStorageSettings.RootPath, id);
        
        var typeId = await _filesDao.GetFileTypeIdByMimeTypeAsync(type)
                         ??
                         throw new ArgumentException($"Unsupported file type: { type }");
        
        await File.WriteAllBytesAsync(path, content);
        
        var relativePath = Path.GetRelativePath(_filesStorageSettings.RootPath, path);
        
        var dbo = new FileDbo
        {
            Id = id,
            Type = new FileTypeDbo { Id = typeId },
            OriginalName = name,
            StoragePath = relativePath,
            Hash = SHA512Helper.CalculateSHA512(content)
        };
        
        var savedDbo = await _filesDao.CreateFileAsync(dbo);
        
        return new FileInfo(savedDbo.Id, savedDbo.OriginalName);
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