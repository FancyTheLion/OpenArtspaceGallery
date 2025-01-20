using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Services.Abstract;

/// <summary>
/// Service for working with imeges sizes
/// </summary>
public interface IImagesSizesService
{
    #region Collection Getters 

    /// <summary>
    /// Get the images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync();
    
    #endregion
    
    #region Is exists

    /// <summary>
    /// Is image size exists by id
    /// </summary>
    Task<bool> IsImageSizeExistsByIdAsync(Guid sizeId);
    
    /// <summary>
    /// Is exist by name
    /// </summary>
    Task<bool> IsExistByNameAsync(string imageSizeName);
    
    /// <summary>
    /// Is exist by dimensions
    /// </summary>
    Task<bool> IsExistByDimensionsAsync(int width, int height);
    
    /// <summary>
    /// Image size is exist
    /// </summary>
    Task<bool> IsImageSizeExistsAsync(string name, int width, int height);

    #endregion
    
    #region Create

    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSize> AddImageSizeAsync(ImageSize imageSize);

    #endregion
    
    #region Delete

    /// <summary>
    /// Delete image size
    /// </summary>
    Task DeleteImageSizeAsync(Guid sizeId);

    #endregion
    
    #region Update

    /// <summary>
    /// Update image size by id
    /// </summary>
    Task<ImageSize> UpdateImageSizeByIdAsync(ImageSize imageSize);

    #endregion
}