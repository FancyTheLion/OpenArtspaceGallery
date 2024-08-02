using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class RenameAlbumDto
{
    /// <summary>
    /// New name album
    /// </summary>
    [JsonPropertyName("newName")]
    public string NewName { get; set; }
}