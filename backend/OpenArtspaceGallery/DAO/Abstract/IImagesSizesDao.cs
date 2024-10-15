using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.DAO.Abstract;

// DAO to work with images sizes
public interface IImagesSizesDao
{
    /// <summary>
    /// Getting images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSizeDbo>> GetImagesSizesAsync();
    
    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSizeDbo> AddImageSizeAsync(ImageSizeDbo imageSizeToInsert);
    
    /// <summary>
    /// Check if image size with given name exists
    /// </summary>
    Task<bool> IsImageSizeExistsByNameAsync(string name);
    
    /// <summary>
    /// Check if image size with given dimensions exists
    /// </summary>
    Task<bool> IsImageSizeExistsByDimensionsAsync(int sizeWidth, int sizeHeight);

    /// <summary>
    /// Delete image size
    /// </summary>
    public Task DeleteImageSizeAsync(Guid sizeId);

    /// <summary>
    /// Is image size exists
    /// </summary>
    public Task<bool> IsImageSizeExistsAsync(Guid sizeId);
    
    /// <summary>
    /// Update image size
    /// </summary>
    Task<ImageSizeDbo> UpdateImageSizeAsync(ImageSizeDbo imageSize);
}