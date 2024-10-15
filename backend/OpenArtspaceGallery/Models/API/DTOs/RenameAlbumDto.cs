using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class RenameAlbumDto
{
    /// <summary>
    /// Album new name
    /// </summary>
    [JsonPropertyName("newName")]
    public string NewName { get; set; }
}