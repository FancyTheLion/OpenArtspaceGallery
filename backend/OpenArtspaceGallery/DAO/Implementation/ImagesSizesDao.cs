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

    public async Task<ImageSizeDbo> AddImageSizeAsync(ImageSizeDbo imageSizeToInsert)
    {
        _ = imageSizeToInsert ?? throw new ArgumentNullException(nameof(imageSizeToInsert), "Image size can't be null!");
        
         await _dbContext
            .ImagesSizes
            .AddAsync(imageSizeToInsert);
        
         await _dbContext.SaveChangesAsync();

         return imageSizeToInsert;
    }

    public async Task<bool> CheckDuplicatesImageSizeAsync(ImageSizeDbo checkImageSize)
    {
        return !await _dbContext
            .ImagesSizes
            .AnyAsync(n => n.Name == checkImageSize.Name);
    }
}