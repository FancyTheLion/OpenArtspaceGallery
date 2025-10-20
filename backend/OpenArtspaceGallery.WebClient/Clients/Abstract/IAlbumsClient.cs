using OpenArtspaceGallery.Models.Albums;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Responses.Albums;

namespace OpenArtspaceGallery.WebClient.Clients.Abstract;

/// <summary>
/// Interface to work with albums
/// </summary>
public interface IAlbumsClient
{
    /// <summary>
    /// Create new album
    /// </summary>
    Task<NewAlbumResponse> CreateAlbumAsync(NewAlbumRequest request);
}