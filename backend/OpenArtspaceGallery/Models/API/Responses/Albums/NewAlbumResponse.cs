using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

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