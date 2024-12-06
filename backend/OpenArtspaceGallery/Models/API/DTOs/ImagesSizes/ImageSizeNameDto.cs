using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeNameDto
{
    /// <summary>
    /// Size name 
    /// </summary>
    [JsonPropertyName("name")]
    public String Name { get; set; }
}