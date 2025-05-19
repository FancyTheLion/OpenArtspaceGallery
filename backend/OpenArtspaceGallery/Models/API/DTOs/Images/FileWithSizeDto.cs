using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class FileWithSizeDto
{
    /// <summary>
    /// File id
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// File size
    /// </summary>
    [JsonPropertyName("size")]
    public ImageSizeShortDto Size { get; set; }
}