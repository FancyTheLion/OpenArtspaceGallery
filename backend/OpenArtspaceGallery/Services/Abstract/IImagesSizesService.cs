using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IImagesSizesService
{
    /// <summary>
    /// Getting the images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync();
    
    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSize> AddImageSizeAsync(ImageSize imageSize);

    /// <summary>
    /// Delete image size
    /// </summary>
    public Task DeleteImageSizeAsync(Guid sizeId);

    /// <summary>
    /// Is image size existt
    /// </summary>
    public Task<bool> IsImageSizeExistsAsync(Guid sizeId);
}