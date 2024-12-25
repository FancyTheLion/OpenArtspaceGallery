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
    /// Is image size exists by ID
    /// </summary>
    public Task<bool> IsImageSizeExistsByIdAsync(Guid sizeId);
    
    /// <summary>
    /// Update image size
    /// </summary>
    Task<ImageSizeDbo> UpdateImageSizeByIdAsync(ImageSizeDbo imageSize);
    
    /// <summary>
    /// Is image size exists by name, width, height
    /// </summary>
    public Task<bool> IsImageSizeExistsByFieldsAsync(string name, int width, int height);
    
    /// <summary>
    /// Update image size by name, width, height fields
    /// </summary>
    public Task<UpdateImageSizeDbo> UpdateImageSizeAsync(UpdateImageSizeDbo updateImageSizeDbo);
}