using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

public class AlbumsListResponse
{
    /// <summary>
    /// Album list
    /// </summary>
    [JsonPropertyName("albums")]
    public IReadOnlyCollection<AlbumDto> Albums { get; private set; }

    public AlbumsListResponse
    (
        IReadOnlyCollection<AlbumDto> albums
    )
    {
        Albums = albums ?? throw new ArgumentNullException(nameof(albums), "Albums collection can't be null.");
    }
}