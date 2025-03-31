using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.Helpers.Hashing;
using OpenArtspaceGallery.Infrastructure.FileStorage;
using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class FilesService : IFilesService
{
    private readonly IFilesDao _filesDao;
    public FilesService
    (
         IFilesDao filesDao
    )
    {
        _filesDao = filesDao;
    }

    public const string FolderPath = "/home/fancy/Projects/OpenArtspaceGalleryStorage";
    public async Task<FileInfoDto> UploadFileAsync(IFormFile file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null!");

        #region Storage file name

        var fileId = Guid.NewGuid();
        var fileName = fileId.ToString();

        #endregion
        
        var filePath = FileStorageHelper.GetFilePath(FolderPath, fileName);
        
        var content = new byte[file.Length];

        await using (var fileStream = file.OpenReadStream())
        {
            await fileStream.ReadAsync(content, 0, (int)file.Length);
        }

        await File.WriteAllBytesAsync(filePath, content);
        
        var storagePath = Path.GetRelativePath(FolderPath, filePath);
        
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
}