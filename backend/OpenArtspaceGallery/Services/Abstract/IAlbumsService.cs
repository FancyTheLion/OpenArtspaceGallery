using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IAlbumsService
{
    /// <summary>
    /// Does album exists?
    /// </summary>
    Task<bool> IsAlbumExistsAsync(Guid albumId);
    
    /// <summary>
    /// Get a list of child albums
    /// </summary>
    Task<IReadOnlyCollection<Album>> GetChildrenAsync(Guid? parentAlbumId);

    /// <summary>
    /// Getting the album hierarchy
    /// </summary>
    Task<IReadOnlyCollection<AlbumInHierarchy>> GetAlbumsHierarchyAsync(Guid albumId);

    /// <summary>
    /// Create a new album
    /// </summary>
    Task<Album> CreateNewAlbumAsync(NewAlbum newAlbum);
    
    /// <summary>
    /// Delete album
    /// </summary>
    Task DeleteAlbumAsync (Guid albumId);

    /// <summary>
    /// Rename albom
    /// </summary>
    public Task RenameAlbumAsync(Guid albumId, string newName);
}