using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;

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
        file.Type = await _dbContext.FileTypes.SingleAsync(ft => ft.Id == file.Type.Id);
        
        await _dbContext
            .Files
            .AddAsync(file);
        
        await _dbContext.SaveChangesAsync();

        return file;
    }
    
    public async Task<FileTypeDbo> GetFileTypeByMimeTypeAsync(string mimeType)
    {
        return await _dbContext.FileTypes.SingleOrDefaultAsync(ft => ft.MimeType == mimeType);
    }

    public async Task<Guid?> GetFileTypeIdByMimeTypeAsync(string mimeType)
    {
        return
            (await _dbContext
                .FileTypes
                .Select(ft => new { Id = ft.Id, Type = ft.MimeType })
                .SingleOrDefaultAsync(ft => ft.Type == mimeType))
            .Id;
    }
}