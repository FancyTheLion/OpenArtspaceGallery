using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.API.Requests.Albums;

public class RenameAlbumRequest
{
    /// <summary>
    /// Request to rename album
    /// </summary>
    [JsonPropertyName("renameAlbumInfo")]
    public RenameAlbumDto RenameAlbumInfo { get; set; }
}