using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class SiteInfoSettings
{
    /// <summary>
    /// Site sources are here
    /// </summary>
    [JsonPropertyName("SourcesUrl")]
    public string SourcesUrl { get; set; }
}