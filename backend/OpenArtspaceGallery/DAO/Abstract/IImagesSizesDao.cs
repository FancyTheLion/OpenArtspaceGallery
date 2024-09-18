using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IImagesSizesDao
{
    /// <summary>
    /// Getting the images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSizeDbo>> GetImagesSizesAsync();
    
    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSizeDbo> AddImageSizeAsync(ImageSizeDbo imageSizeToInsert);
    
    /// <summary>
    /// Validate image size for duplicates by name
    /// </summary>
    Task<bool> IsImageSizeExistsByNameAsync(string name);
    
    /// <summary>
    /// Validate image size for duplicates by size
    /// </summary>
    Task<bool> IsImageSizeExistsByDimensionsAsync(int sizeWidth, int sizeHeight);
}