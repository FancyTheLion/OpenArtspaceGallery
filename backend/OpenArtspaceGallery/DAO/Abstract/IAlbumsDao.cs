using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.DAO.Abstract;

/// <summary>
/// DAO to work with albums
/// </summary>
public interface IAlbumsDao
{
    #region Get

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

    /// <summary>
    /// Get album info (returns null if there is no such album)
    /// </summary>
    Task<AlbumDbo?> GetAlbumByIdAsync(Guid albumId);
    
    #endregion
    
    #region Is exists

    /// <summary>
    /// Does album exists?
    /// </summary>
    Task<bool> IsExistsAsync(Guid albumId);

    #endregion
    
    #region Create

    /// <summary>
    /// Add new album
    /// </summary>
    Task<AlbumDbo?> CreateAsync(AlbumDbo? albumToInsert);

    #endregion
    
    #region Delete

    /// <summary>
    /// Delete album
    /// </summary>
    Task DeleteAsync(Guid albumToDelete);

    #endregion
    
    #region Rename

    /// <summary>
    /// Rename album
    /// </summary>
    Task RenameAsync(Guid albumId, string newName);

    #endregion
}