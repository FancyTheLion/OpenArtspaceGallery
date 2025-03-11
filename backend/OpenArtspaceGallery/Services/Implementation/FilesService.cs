using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.Helpers.Hashing;
using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class FilesService : IFilesService
{
    private readonly MainDbContext _dbContext;
    private readonly IFilesDao _filesDao;
    public FilesService
    (
         MainDbContext dbContext,
         IFilesDao filesDao
    )
    {
        _dbContext = dbContext;
        _filesDao = filesDao;
    }

    public const string FolderPath = "/home/fancy/Projects/OpenArtspaceGalleryStorage";
    public async Task<FileInfoDto> UploadFileAsync(IFormFile file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null!");

        var filePath = Path.Combine(FolderPath, file.FileName);
        
        var content = new byte[file.Length];

        using (var fileStream = file.OpenReadStream())
        {
            await fileStream.ReadAsync(content, 0, (int)file.Length);
        }

        await File.WriteAllBytesAsync(filePath, content);
        
        var fileType = await _filesDao.GetFileTypeByMimeTypeAsync(file.ContentType);
        
        if (fileType == null)
        {
            throw new ArgumentException($"Unsupported file type: {file.ContentType}");
        }
        
        _dbContext.Entry(fileType).State = EntityState.Unchanged;
        
        var fileDbo = new FileDbo()
        {
            Type = fileType,
            OriginalName = file.FileName,
            StoragePath = FolderPath,
            Hash = SHA512Helper.CalculateSHA512(content)
        };

        await _filesDao.CreateFileAsync(fileDbo);

        return new FileInfoDto(fileDbo.Id, fileDbo.OriginalName);
    }
}