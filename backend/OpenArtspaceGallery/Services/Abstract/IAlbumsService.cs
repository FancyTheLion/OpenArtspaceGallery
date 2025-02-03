using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

/// <summary>
/// Service for working with albums
/// </summary>
public interface IAlbumsService
{
    #region Collection Getters 

    /// <summary>
    /// Get a list of child albums
    /// </summary>
    Task<IReadOnlyCollection<Album>> GetChildrenAsync(Guid? parentAlbumId);
    
    /// <summary>
    /// Getting the album hierarchy
    /// </summary>
    Task<IReadOnlyCollection<AlbumInHierarchy>> GetAlbumsHierarchyAsync(Guid albumId);

    #endregion
    
    #region Is exists 
    
    /// <summary>
    /// Does album exists?
    /// </summary>
    Task<bool> IsExistsAsync(Guid albumId);
    
    #endregion

    #region Create 

    /// <summary>
    /// Create a new album
    /// </summary>
    Task<Album> CreateAsync(NewAlbum newAlbum);

    #endregion

    #region Delete

    /// <summary>
    /// Delete album
    /// </summary>
    Task DeleteAsync (Guid albumId);

    #endregion

    #region Rename

    /// <summary>
    /// Rename albom
    /// </summary>
    Task RenameAsync(Guid albumId, string newName);

    #endregion
}