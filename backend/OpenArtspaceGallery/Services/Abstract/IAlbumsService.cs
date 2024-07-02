using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IAlbumsService
{
    /// <summary>
    /// Get a list of child albums
    /// </summary>
    Task<IReadOnlyCollection<Album>> GetChildrenAsync(Guid? parentAlbumId);
}