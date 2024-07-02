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
}