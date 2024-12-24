using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Services.Abstract;

/// <summary>
/// Service for working with imeges sizes
/// </summary>
public interface IImagesSizesService
{
    /// <summary>
    /// Get the images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync();
    
    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSize> AddImageSizeAsync(ImageSize imageSize);

    /// <summary>
    /// Delete image size
    /// </summary>
    Task DeleteImageSizeAsync(Guid sizeId);

    /// <summary>
    /// Is image size exists
    /// </summary>
    Task<bool> IsImageSizeExistsAsync(Guid sizeId);
    
    /// <summary>
    /// Update image size
    /// </summary>
    Task<ImageSize> UpdateImageSizeAsync(ImageSize imageSize);
    
    /// <summary>
    /// Is exist by name
    /// </summary>
    Task<bool> IsExistByNameAsync(string imageSizeName);
    
    /// <summary>
    /// Is exist by name
    /// </summary>
    Task<bool> IsExistByDimensionsAsync(int width, int height);
    
    /// <summary>
    /// Image size is exist
    /// </summary>
    Task<bool> IsImageSizeExistsAsync(string name, int width, int height);
}