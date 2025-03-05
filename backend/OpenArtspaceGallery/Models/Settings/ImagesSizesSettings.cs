using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class ImagesSizesSettings
{
    /// <summary>
    /// Minimum image size
    /// </summary>
    [JsonPropertyName("MinWidth")]
    public int MinWidth { get; init; }
    
    /// <summary>
    /// Maximum image size
    /// </summary>
    [JsonPropertyName("MaxWidth")]
    public int MaxWidth { get; init; }
    
    /// <summary>
    /// Minimum image size
    /// </summary>
    [JsonPropertyName("MinHeight")]
    public int MinHeight { get; init; }
    
    /// <summary>
    /// Maximum image size
    /// </summary>
    [JsonPropertyName("MaxHeight")]
    public int MaxHeight { get; init; }
}