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
    /// Add new album to the database
    /// </summary>
    Task<AlbumDbo> CreateNewAlbumAsync(AlbumDbo albumToInsert);
    
    /// <summary>
    /// Delete album from database
    /// </summary>
    Task DeleteAlbumAsync(Guid albumToInsert);
    
    /// <summary>
    /// Get albums id by parent Id
    /// </summary>
    Task<IReadOnlyCollection<Guid>> GetChildrenAlbumbsGuidsAsync(Guid albumId);
}