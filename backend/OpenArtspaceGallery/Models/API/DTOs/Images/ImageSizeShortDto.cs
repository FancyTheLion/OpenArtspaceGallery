using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class ImageSizeShortDto
{
    /// <summary>
    /// Size id
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Size type
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }
    
    /// <summary>
    /// Size name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}