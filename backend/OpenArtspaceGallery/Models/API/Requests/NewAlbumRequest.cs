using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Requests;

public class NewAlbumRequest
{
    /// <summary>
    /// Request to add album to backend
    /// </summary>
    [JsonPropertyName("albumToAdd")]
    public NewAlbumDto AlbumToAdd { get; set; }
}