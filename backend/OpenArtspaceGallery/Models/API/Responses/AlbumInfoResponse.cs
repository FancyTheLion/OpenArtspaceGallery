using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

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