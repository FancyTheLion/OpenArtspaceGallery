using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class NewAlbumResponse
{
    /// <summary>
    /// New album
    /// </summary>
    [JsonPropertyName("newAlbum")]
    public AlbumDto NewAlbum { get; private set; } 

    public NewAlbumResponse
    (
        AlbumDto newAlbum
    )
    {
        NewAlbum = newAlbum ?? throw new ArgumentNullException(nameof(newAlbum), "New album can't be null.");
    }
}