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

    public async Task<bool> IsAnotherImageSizeExistsByNameAsync(string name)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name), "Name can't be null!");
        
        return await _dbContext
            .ImagesSizes
            .AnyAsync(n => n.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> IsAnotherImageSizeExistsByDimensionsAsync(int sizeWidth, int sizeHeight)
    {
        return await _dbContext
            .ImagesSizes
            .AnyAsync(s => s.Width == sizeWidth && s.Height == sizeHeight);
    }

    public async Task DeleteImageSizeAsync(Guid sizeId)
    {
        var imageSize = await _dbContext
            .ImagesSizes
            .SingleAsync(s => s.Id == sizeId);

        _dbContext.Remove(imageSize);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsImageSizeExistsByIdAsync(Guid sizeId)
    {
        return await _dbContext
            .ImagesSizes
            .AnyAsync(s => s.Id == sizeId);
    }

    public async Task<ImageSizeDbo> UpdateImageSizeByIdAsync(ImageSizeDbo updateImageSize)
    {
        _ = updateImageSize ?? throw new ArgumentNullException(nameof(updateImageSize), "Image size to update can't be null!");
        
        var imageSize = await _dbContext
            .ImagesSizes
            .SingleAsync(s => s.Id == updateImageSize.Id);
    
        imageSize.Name = updateImageSize.Name;
        imageSize.Width = updateImageSize.Width;
        imageSize.Height = updateImageSize.Height;
    
        await _dbContext.SaveChangesAsync();

        return imageSize;
    }

    public async Task<bool> IsImageSizeExistsByPropertiesAsync(string name, int width, int height)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name), "Name can't be null!");
        
        return await _dbContext
            .ImagesSizes
            .AnyAsync(f => f.Name.ToLower() == name.ToLower() && f.Width == width && f.Height == height);
    }
    
    /*public async Task<bool> IsImageSizeExistsAsync(Guid sizeId, string name, int width, int height)
    {
        return await _dbContext
            .ImagesSizes
            .AnyAsync(i => i.Id == sizeId && i.Name.ToLower() == name.ToLower() && i.Width == width && i.Height == height);
    }*/
}