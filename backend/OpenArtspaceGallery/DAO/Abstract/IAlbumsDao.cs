using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.DAO.Abstract;

/// <summary>
/// DAO to work with albums
/// </summary>
public interface IAlbumsDao
{
    #region Collection Getters

    /// <summary>
    /// Get albums by parent Id
    /// </summary>
    Task<IReadOnlyCollection<AlbumDbo>> GetChildrenAsync(Guid? parentAlbumId);
    
    /// <summary>
    /// Getting the album hierarchy
    /// </summary>
    Task<IReadOnlyCollection<AlbumDbo>> GetAlbumsHierarchyAsync(Guid albumId);
    
    /// <summary>
    /// Get albums id by parent Id
    /// </summary>
    Task<IReadOnlyCollection<Guid>> GetChildrenAlbumbsGuidsAsync(Guid albumId);

    #endregion
    
    #region Is exists

    /// <summary>
    /// Does album exists?
    /// </summary>
    Task<bool> IsAlbumExistsAsync(Guid albumId);

    #endregion
    
    #region Create

    /// <summary>
    /// Add new album
    /// </summary>
    Task<AlbumDbo> CreateNewAlbumAsync(AlbumDbo albumToInsert);

    #endregion
    
    #region Delete

    /// <summary>
    /// Delete album
    /// </summary>
    Task DeleteAlbumAsync(Guid albumToDelete);

    #endregion
    
    #region Rename

    /// <summary>
    /// Rename album
    /// </summary>
    Task RenameAlbumAsync(Guid albumId, string newName);

    #endregion
}