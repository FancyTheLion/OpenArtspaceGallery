using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeDimensionsDto
{
    /// <summary>
    /// Image size width dimension
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    /// <summary>
    /// Image size height dimension 
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
}