using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

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