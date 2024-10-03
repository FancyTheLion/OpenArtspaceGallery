using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

// TODO: Add comment
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
}