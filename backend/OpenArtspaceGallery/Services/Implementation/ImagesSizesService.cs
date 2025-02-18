using Microsoft.AspNetCore.Http.HttpResults;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class ImagesSizesService : IImagesSizesService
{
    private readonly IImagesSizesDao _imagesSizesDao;

    public ImagesSizesService
    (
        IImagesSizesDao imagesSizesDao
    )
    {
        _imagesSizesDao = imagesSizesDao;
    }

    public async Task<IReadOnlyCollection<ImageSize>> GetListAsync()
    {
        return (await _imagesSizesDao.GetListAsync())
            .Select(i => ImageSize.FromDbo(i))
            .ToList();
    }

    public async Task<ImageSize> GetImageSizeByIdAsync(Guid id)
    {
        return await _imagesSizesDao.GetImageSizeByIdAsync(id);
    }

    public async Task<ImageSize> AddAsync(ImageSize imageSize)
    {
        if (imageSize == null)
        {
            throw new ArgumentNullException(nameof(imageSize), "New image size data cannot be null!");
        }
        
        await ValidateImageSizeAsync(imageSize);
        
        var newImageSize = new ImageSizeDbo()
        {
            Id = Guid.Empty,
            Name = imageSize.Name,
            Width = imageSize.Width,
            Height = imageSize.Height
        };
        
        return ImageSize.FromDbo(await _imagesSizesDao.AddAsync(newImageSize));
    }

    public async Task DeleteAsync(Guid sizeId)
    {
        await _imagesSizesDao.DeleteAsync(sizeId);
    }
    
    public async Task<bool> IsExistsByIdAsync(Guid sizeId)
    {
        return await _imagesSizesDao.IsExistsByIdAsync(sizeId);
    }

    public async Task<ImageSize> UpdateByIdAsync(ImageSize imageSize)
    {
        _ = imageSize ?? throw new ArgumentNullException(nameof(imageSize), "Update data cannot be null!");
        
        await ValidateImageSizeAsync(imageSize);
            
        var imageSizeToUpdate = new ImageSizeDbo()
        {
            Id = imageSize.Id,
            Name = imageSize.Name,
            Width = imageSize.Width,
            Height = imageSize.Height
        };
        
        return ImageSize.FromDbo(await _imagesSizesDao.UpdateByIdAsync(imageSizeToUpdate));
    }

    /// <summary>
    /// Validate image size for duplicates
    /// </summary>
    private async Task ValidateImageSizeAsync(ImageSize imageSize)
    {
        if (await _imagesSizesDao.IsAnotherExistsByNameAsync(imageSize.Id, imageSize.Name))
        {
            throw new ArgumentException($"Image size with name { imageSize.Name } already exists.", nameof(imageSize.Name));
        }
        
        if (await _imagesSizesDao.IsAnotherExistsByDimensionsAsync(imageSize.Id, imageSize.Width, imageSize.Height))
        {
            throw new ArgumentException($"Image size with width { imageSize.Width } and height { imageSize.Height } already exists.");
        }
    }
    
    public async Task<bool> IsExistByNameAsync(string imageSizeName)
    {
        _ = imageSizeName ?? throw new ArgumentNullException(nameof(imageSizeName), "Image size name cannot be null!");
        
        return await _imagesSizesDao.IsAnotherExistsByNameAsync(Guid.Empty, imageSizeName);
    }
    public async Task<bool> IsExistByDimensionsAsync(int width, int height)
    {
        return await _imagesSizesDao.IsAnotherExistsByDimensionsAsync(Guid.Empty, width, height);
    }

    public async Task<bool> IsImageSizeExistsAsync(string name, int width, int height)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name), "Image size name cannot be null!");
        
        return await _imagesSizesDao.IsExistsByPropertiesAsync(name, width, height);
    }
}