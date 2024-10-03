using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Requests;

public class RenameAlbumRequest
{
    /// <summary>
    /// Request to rename album
    /// </summary>
    [JsonPropertyName("renameAlbumInfo")]
    public RenameAlbumDto RenameAlbumInfo { get; set; }
}