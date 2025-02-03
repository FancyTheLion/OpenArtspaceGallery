using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.DAO.Abstract;

// DAO to work with images sizes
public interface IImagesSizesDao
{
    #region Collection Getters

    /// <summary>
    /// Getting images sizes list
    /// </summary>
    Task<IReadOnlyCollection<ImageSizeDbo>> GetListAsync();

    #endregion
    
    #region Is exists

    /// <summary>
    /// Check if another image size with given name exists
    /// </summary>
    Task<bool> IsAnotherExistsByNameAsync(Guid thisImageId, string name);
    
    /// <summary>
    /// Check if another image size with given dimensions exists
    /// </summary>
    Task<bool> IsAnotherExistsByDimensionsAsync(Guid thisImageId, int sizeWidth, int sizeHeight);
    
    /// <summary>
    /// Is image size exists by ID
    /// </summary>
    public Task<bool> IsExistsByIdAsync(Guid sizeId);
    
    /// <summary>
    /// Is image size exists by name, width, height fields
    /// </summary>
    public Task<bool> IsExistsByPropertiesAsync(string name, int width, int height);
    
    #endregion
    
    #region Create

    /// <summary>
    /// Add new image size
    /// </summary>
    Task<ImageSizeDbo> AddAsync(ImageSizeDbo imageSizeToInsert);

    #endregion
    
    #region Delete

    /// <summary>
    /// Delete image size
    /// </summary>
    public Task DeleteAsync(Guid sizeId);

    #endregion
    
    #region Update

    /// <summary>
    /// Update image size by id
    /// </summary>
    Task<ImageSizeDbo> UpdateByIdAsync(ImageSizeDbo imageSize);

    #endregion
}