using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.API.Requests.Albums;

public class NewAlbumRequest
{
    /// <summary>
    /// Request to add album to backend
    /// </summary>
    [JsonPropertyName("albumToAdd")]
    public NewAlbumDto AlbumToAdd { get; set; }
}