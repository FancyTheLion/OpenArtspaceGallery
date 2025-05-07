using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;

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

    public async Task<IReadOnlyCollection<ImageSizeDbo>> GetListAsync()
    {
        return await _dbContext
            .ImagesSizes
            .ToListAsync();
    }

    public async Task<ImageSizeDbo?> GetImageSizeByIdAsync(Guid id)
    {
        return await _dbContext
            .ImagesSizes
            .SingleOrDefaultAsync(ims => ims.Id == id);
    }

    public async Task<ImageSizeDbo?> GetImageSizeOriginalAsync()
    {
        return await _dbContext
            .ImagesSizes
            .SingleAsync(s => s.Type == ImagesSizesTypes.Original);
    }

    public async Task<ImageSizeDbo> AddAsync(ImageSizeDbo imageSizeToInsert)
    {
        _ = imageSizeToInsert ?? throw new ArgumentNullException(nameof(imageSizeToInsert), "Image size can't be null!");
        
         await _dbContext
            .ImagesSizes
            .AddAsync(imageSizeToInsert);
        
         await _dbContext.SaveChangesAsync();

         return imageSizeToInsert;
    }

    public async Task<bool> IsAnotherExistsByNameAsync(Guid thisImageId, string name)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name), "Name can't be null!");
        
        return await _dbContext
            .ImagesSizes
            .AnyAsync(s => s.Name.ToLower() == name.ToLower() && s.Id != thisImageId);
    }

    public async Task<bool> IsAnotherExistsByDimensionsAsync(Guid thisImageId, int sizeWidth, int sizeHeight)
    {
        return await _dbContext
            .ImagesSizes
            .AnyAsync(s => s.Width == sizeWidth && s.Height == sizeHeight && s.Id != thisImageId);
    }

    public async Task DeleteAsync(Guid sizeId)
    {
        var imageSize = await _dbContext
            .ImagesSizes
            .SingleAsync(s => s.Id == sizeId);

        _dbContext.Remove(imageSize);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsExistsByIdAsync(Guid sizeId)
    {
        return await _dbContext
            .ImagesSizes
            .AnyAsync(s => s.Id == sizeId);
    }

    public async Task<ImageSizeDbo> UpdateByIdAsync(ImageSizeDbo updateImageSize)
    {
        _ = updateImageSize ?? throw new ArgumentNullException(nameof(updateImageSize), "Image size to update can't be null!");
        
        var imageSize = await _dbContext
            .ImagesSizes
            .SingleAsync(s => s.Id == updateImageSize.Id);
    
        imageSize.Name = updateImageSize.Name;
        imageSize.Width = updateImageSize.Width;
        imageSize.Height = updateImageSize.Height;
        imageSize.Type = updateImageSize.Type;
    
        await _dbContext.SaveChangesAsync();

        return imageSize;
    }

    public async Task<bool> IsExistsByPropertiesAsync(string name, int width, int height)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name), "Name can't be null!");
        
        return await _dbContext
            .ImagesSizes
            .AnyAsync(f => f.Name.ToLower() == name.ToLower() && f.Width == width && f.Height == height);
    }
}