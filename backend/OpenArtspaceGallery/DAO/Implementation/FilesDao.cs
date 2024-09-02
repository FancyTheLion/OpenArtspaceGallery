using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Files;

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

    /*public async Task<IReadOnlyCollection<FileDbo>> GetImagesSizesAsync()
    {
        return await _dbContext.ImagesSizes.Select().ToListAsync();
    }*/
}