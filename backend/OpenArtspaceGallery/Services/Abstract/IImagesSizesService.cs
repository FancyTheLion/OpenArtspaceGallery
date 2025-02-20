using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Services.Abstract;

/// <summary>
/// Service for working with images sizes
/// </summary>
public interface IImagesSizesService
{
    #region Get

    /// <summary>
    /// Get the images sizes list
    /// </summary>
    Task<IReadOnlyCollection<ImageSize?>> GetListAsync();
    
    /// <summary>
    /// Get image size by ID
    /// </summary>
    Task<ImageSize?> GetImageSizeByIdAsync(Guid id);
    
    #endregion
    
    #region Is exists

    /// <summary>
    /// Is image size exists by id
    /// </summary>
    Task<bool> IsExistsByIdAsync(Guid sizeId);
    
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
    Task<ImageSize> AddAsync(ImageSize imageSize);

    #endregion
    
    #region Delete

    /// <summary>
    /// Delete image size
    /// </summary>
    Task DeleteAsync(Guid sizeId);

    #endregion
    
    #region Update

    /// <summary>
    /// Update image size by id
    /// </summary>
    Task<ImageSize> UpdateByIdAsync(ImageSize imageSize);

    #endregion
}