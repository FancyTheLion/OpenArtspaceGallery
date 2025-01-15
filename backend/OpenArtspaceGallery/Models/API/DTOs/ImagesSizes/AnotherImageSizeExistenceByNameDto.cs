using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class AnotherImageSizeExistenceByNameDto
{
    /// <summary>
    /// Size name 
    /// </summary>
    [JsonPropertyName("name")]
    public String Name { get; set; }
}