using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class SiteInfoSettings
{
    /// <summary>
    /// Site sources are here
    /// </summary>
    [JsonPropertyName("SourcesUrl")]
    public string SourcesUrl { get; set; }
    
    /// <summary>
    /// Site version are here
    /// </summary>
    [JsonPropertyName("Version")]
    public string Version { get; set; }
}