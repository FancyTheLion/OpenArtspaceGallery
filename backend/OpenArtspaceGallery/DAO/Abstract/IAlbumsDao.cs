using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.DAO.Abstract;

/// <summary>
/// DAO to work with albums
/// </summary>
public interface IAlbumsDao
{
    /// <summary>
    /// Get albums by parent Id
    /// </summary>
    Task<IReadOnlyCollection<AlbumDbo>> GetChildrenAsync(Guid? parentAlbumId);

    /// <summary>
    /// Getting the album hierarchy
    /// </summary>
    Task<IReadOnlyCollection<AlbumDbo>> GetAlbumsHierarchyAsync(Guid albumId);
    
    /// <summary>
    /// Does album exists?
    /// </summary>
    Task<bool> IsAlbumExistsAsync(Guid albumId);

    /// <summary>
    /// Add new album
    /// </summary>
    Task<AlbumDbo> CreateNewAlbumAsync(AlbumDbo albumToInsert);
    
    /// <summary>
    /// Delete album
    /// </summary>
    Task DeleteAlbumAsync(Guid albumToDelete);
    
    /// <summary>
    /// Get albums id by parent Id
    /// </summary>
    Task<IReadOnlyCollection<Guid>> GetChildrenAlbumbsGuidsAsync(Guid albumId);
    
    /// <summary>
    /// Rename album
    /// </summary>
    Task RenameAlbumAsync(Guid albumId, string newName);
}