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
    
    public FilesService
    (
        IOptions<FilesStorageSettings> filesStorageSettings,
        IFilesDao filesDao
    )
    {
        _filesStorageSettings = filesStorageSettings.Value;
        _filesDao = filesDao;
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

        await File.WriteAllBytesAsync(filePath, content);
        
        var storagePath = Path.GetRelativePath(_filesStorageSettings.RootPath, filePath);
        
        var fileTypeId = await _filesDao.GetFileTypeIdByMimeTypeAsync(file.ContentType);
        
        if (fileTypeId == null)
        {
            throw new ArgumentException($"Unsupported file type: {file.ContentType}");
        }
        
        var fileDbo = new FileDbo()
        {
            Id = fileId,
            Type = new FileTypeDbo() { Id = fileTypeId.Value },
            OriginalName = file.FileName,
            StoragePath = storagePath,
            Hash = SHA512Helper.CalculateSHA512(content)
        };

        var result = await _filesDao.CreateFileAsync(fileDbo);

        return new FileInfoDto(result.Id, result.OriginalName);
    }

    public async Task<FileDownloadDto> GetFileForDownloadAsync(Guid fileId)
    {
        var fileWithType = await _filesDao.GetFileWithTypeAsync(fileId);
        
        var absolutePath = Path.Combine(_filesStorageSettings.RootPath, fileWithType.StoragePath);

        var fileBytes = await File.ReadAllBytesAsync(absolutePath);

        return new FileDownloadDto
        {
            Id = fileWithType.Id,
            Content = fileBytes,
            OriginalName = fileWithType.OriginalName,
            StoragePath = fileWithType.StoragePath,
            Type = fileWithType.Type,
            Hash = fileWithType.Hash,
            LastModificationTime = fileWithType.LastModificationTime
        };
    }
}