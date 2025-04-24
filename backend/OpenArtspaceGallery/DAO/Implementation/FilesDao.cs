using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Implementation;

public class FilesDao : IFilesDao
{
    private readonly MainDbContext _dbContext;

    public FilesDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<FileDbo> CreateFileAsync(FileDbo file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null.");
        
        file.LastModificationTime = DateTime.UtcNow;

        // Looking up for file type by ID
        file.Type = await _dbContext.FilesTypes.SingleAsync(ft => ft.Id == file.Type.Id);
        
        await _dbContext
            .Files
            .AddAsync(file);
        
        await _dbContext.SaveChangesAsync();

        return file;
    }

    public async Task<FileTypeDbo> GetFileTypeByMimeTypeAsync(string mimeType)
    {
        return await _dbContext.FilesTypes.SingleOrDefaultAsync(ft => ft.MimeType == mimeType);
    }

    public async Task<Guid?> GetFileTypeIdByMimeTypeAsync(string mimeType)
    {
        if (string.IsNullOrEmpty(mimeType))
        {
            throw new ArgumentNullException(nameof(mimeType), "MIME type must not be null or empty.");
        }
        
        var fileType = await _dbContext
                .FilesTypes
                .Select(ft => new { Id = ft.Id, Type = ft.MimeType })
                .SingleOrDefaultAsync(ft => ft.Type == mimeType);
            
            return fileType?.Id;
    }

    public async Task<FileDbo?> GetFileMetadataAsync(Guid fileId)
    {
        return await _dbContext.Files
            .Include(f => f.Type)
            .FirstOrDefaultAsync(f => f.Id == fileId);
    }
    
    public async Task<ImageDbo> AddImageAsync(ImageDbo image)
    {
        await _dbContext
            .Images
            .AddAsync(image);
        
        await _dbContext.SaveChangesAsync();

        return image;
    }
    
    public async Task<ImageFileDbo> AddImageFileAsync(ImageFileDbo imageFile)
    {
        await _dbContext
            .Image
            .AddAsync(imageFile);
        
        await _dbContext.SaveChangesAsync();

        return imageFile;
    }
}