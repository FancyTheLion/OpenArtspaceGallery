using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Implementation;

public class ImagesSizesDao : IImagesSizesDao
{
    private readonly MainDbContext _dbContext;
    
    public ImagesSizesDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ImageSizeDbo>> GetImagesSizesAsync()
    {
        return await _dbContext
            .ImagesSizes
            .ToListAsync();
    }
}