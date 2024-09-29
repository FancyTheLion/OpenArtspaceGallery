using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class UpdateImageSizeDto
{
    /// <summary>
    /// Image size id
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Size name (large, medium and others)
    /// </summary>
    [JsonPropertyName("name")]
    public String Name { get; set; }

    /// <summary>
    ///  Image size (Width)
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Image size (Height)
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
}