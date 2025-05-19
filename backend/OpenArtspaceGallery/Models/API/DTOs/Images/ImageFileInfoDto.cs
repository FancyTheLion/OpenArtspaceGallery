using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class ImageFileInfoDto
{
    /// <summary>
    /// Information about the file and its size
    /// </summary>
    [JsonPropertyName("file")]
    public FileWithSizeDto File { get; set; }
}