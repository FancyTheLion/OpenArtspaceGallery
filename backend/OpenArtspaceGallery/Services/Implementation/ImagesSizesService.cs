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

    public async Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync()
    {
        return (await _imagesSizesDao.GetImagesSizesAsync())
            .Select(i => ImageSize.FromDbo(i))
            .ToList();
    }

    public async Task<ImageSize> AddImageSizeAsync(ImageSize imageSize)
    {
        if (imageSize == null)
        {
            throw new ArgumentNullException(nameof(imageSize), "New image size data cannot be null!");
        }
        
        var newImageSize = new ImageSizeDbo()
        {
            Id = Guid.Empty,
            Name = imageSize.Name,
            Width = imageSize.Width,
            Height = imageSize.Height
        };

        await ValidateImageSizeAsync(imageSize);
        
        return ImageSize.FromDbo(await _imagesSizesDao.AddImageSizeAsync(newImageSize));
    }

    public async Task DeleteImageSizeAsync(Guid sizeId)
    {
        await _imagesSizesDao.DeleteImageSizeAsync(sizeId);
    }
    
    public async Task<bool> IsImageSizeExistsAsync(Guid sizeId)
    {
        return await _imagesSizesDao.IsImageSizeExistsAsync(sizeId);
    }

    public async Task<ImageSize> UpdateImageSizeAsync(ImageSize imageSize)
    {
        _ = imageSize ?? throw new ArgumentNullException(nameof(imageSize), "Update data cannot be null!");
        
        var imageSizeToUpdate = new ImageSizeDbo()
        {
            Id = imageSize.Id,
            Name = imageSize.Name,
            Width = imageSize.Width,
            Height = imageSize.Height
        };
        
        await ValidateImageSizeAsync(imageSize);
        
        return ImageSize.FromDbo(await _imagesSizesDao.UpdateImageSizeAsync(imageSizeToUpdate));
    }

    /// <summary>
    /// Validate image size for duplicates
    /// </summary>
    private async Task ValidateImageSizeAsync(ImageSize imageSize)
    {
        if (await _imagesSizesDao.IsImageSizeExistsByNameAsync(imageSize.Name))
        {
            throw new ArgumentException($"Image size with name { imageSize.Name } already exists.", nameof(imageSize.Name));
        }
        
        if (await _imagesSizesDao.IsImageSizeExistsByDimensionsAsync(imageSize.Width, imageSize.Height))
        {
            throw new ArgumentException($"Image size with width { imageSize.Width } and height { imageSize.Height } already exists.");
        }
    }
    
    public async Task<bool> IsExistByNameAsync(string imageSizeName)
    {
        _ = imageSizeName ?? throw new ArgumentNullException(nameof(imageSizeName), "Image size name cannot be null!");
        
        return await _imagesSizesDao.IsImageSizeExistsByNameAsync(imageSizeName);
    }
    public async Task<bool> IsExistByDimensionsAsync(int width, int height)
    {
        return await _imagesSizesDao.IsImageSizeExistsByDimensionsAsync(width, height);
    }

    public async Task<bool> IsImageSizeExistsAsync(string name, int width, int height)
    {
        return await _imagesSizesDao.IsImageSizeExistsByFieldsAsync(name, width, height);
    }
}