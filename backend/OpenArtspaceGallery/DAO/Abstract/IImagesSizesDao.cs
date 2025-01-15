using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.DAO.Abstract;

// DAO to work with images sizes
public interface IImagesSizesDao
{
    #region Collection Getters

    /// <summary>
    /// Getting images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSizeDbo>> GetImagesSizesAsync();

    #endregion
    
    #region Is exists

    /// <summary>
    /// Check if image size with given name exists
    /// </summary>
    Task<bool> IsAnotherImageSizeExistsByNameAsync(Guid id, string name);
    
    /// <summary>
    /// Check if image size with given dimensions exists
    /// </summary>
    Task<bool> IsAnotherImageSizeExistsByDimensionsAsync(Guid id, int sizeWidth, int sizeHeight);
    
    /// <summary>
    /// Is image size exists by ID
    /// </summary>
    public Task<bool> IsImageSizeExistsByIdAsync(Guid sizeId);
    
    /// <summary>
    /// Is image size exists by name, width, height fields
    /// </summary>
    public Task<bool> IsImageSizeExistsByPropertiesAsync(string name, int width, int height);
    
    /*/// <summary>
    /// Is image size exists by id
    /// </summary>
    public Task<bool> IsImageSizeExistsAsync(Guid sizeId, string name, int width, int height);*/

    #endregion
    
    #region Create

    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSizeDbo> AddImageSizeAsync(ImageSizeDbo imageSizeToInsert);

    #endregion
    
    #region Delete

    /// <summary>
    /// Delete image size
    /// </summary>
    public Task DeleteImageSizeAsync(Guid sizeId);

    #endregion
    
    #region Update

    /// <summary>
    /// Update image size by id
    /// </summary>
    Task<ImageSizeDbo> UpdateImageSizeByIdAsync(ImageSizeDbo imageSize);

    #endregion
}