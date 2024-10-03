using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class NewAlbumResponse
{
    [JsonPropertyName("newAlbum")]
    public AlbumDto NewAlbum { get; set; } // TODO: Private set

    public NewAlbumResponse
    (
        AlbumDto newAlbum
    )
    {
        NewAlbum = newAlbum; // TODO: Add validation
    }
}