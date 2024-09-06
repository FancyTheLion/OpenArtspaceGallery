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
}