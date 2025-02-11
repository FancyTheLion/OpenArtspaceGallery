using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

public class AlbumInfoResponse
{
    /// <summary>
    /// Album info
    /// </summary>
    [JsonPropertyName("album")]
    public AlbumDto Album { get; private set; } 

    public AlbumInfoResponse
    (
        AlbumDto album
    )
    {
        Album = album ?? throw new ArgumentNullException(nameof(album), "Album info can't be null.");
    }
}