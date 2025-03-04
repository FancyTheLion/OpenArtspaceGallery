using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class ImageSizeSettings
{
    /// <summary>
    /// Minimum image size
    /// </summary>
    [JsonPropertyName("MinWidth")]
    public int MinWidth { get; set; }
    
    /// <summary>
    /// Maximum image size
    /// </summary>
    [JsonPropertyName("MaxWidth")]
    public int MaxWidth { get; set; }
    
    /// <summary>
    /// Minimum image size
    /// </summary>
    [JsonPropertyName("MinHeight")]
    public int MinHeight { get; set; }
    
    /// <summary>
    /// Maximum image size
    /// </summary>
    [JsonPropertyName("MaxHeight")]
    public int MaxHeight { get; set; }
}