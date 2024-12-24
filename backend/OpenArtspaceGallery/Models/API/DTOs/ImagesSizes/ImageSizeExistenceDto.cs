using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeExistenceDto
{
    /// <summary>
    /// Image size name 
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Image size width 
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    /// <summary>
    /// Image size height  
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
}