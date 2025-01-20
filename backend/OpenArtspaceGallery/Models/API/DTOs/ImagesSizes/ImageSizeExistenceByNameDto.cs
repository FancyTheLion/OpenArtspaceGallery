using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeExistenceByNameDto
{
    /// <summary>
    /// Size name 
    /// </summary>
    [JsonPropertyName("name")]
    public String Name { get; set; }
}