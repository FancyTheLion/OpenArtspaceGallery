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

    public async Task CreateFileAsync(FileDbo file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null.");
        
        file.LastModificationTime = DateTime.UtcNow;
        
        _dbContext.Entry(file.Type).State = EntityState.Unchanged;

        await _dbContext
            .Files
            .AddAsync(file);
        
        var affected = await _dbContext.SaveChangesAsync();
        if (affected != 1)
        {
            throw new InvalidOperationException($"Expected to insert 1 row, actually inserted { affected } rows!");
        }
    }
    
    public async Task<FileTypeDbo> GetFileTypeByMimeTypeAsync(string mimeType)
    {
        return await _dbContext.FileTypes.FirstOrDefaultAsync(ft => ft.MimeType == mimeType);
    }
    
}