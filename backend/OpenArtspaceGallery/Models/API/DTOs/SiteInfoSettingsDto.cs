using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class SiteInfoSettingsDto
{
    /// <summary>
    ///  Backend version
    /// </summary>
    [JsonPropertyName("sourcesLink")]
    public string SourcesLink { get; private set; }
    
    public SiteInfoSettingsDto(string linkSourceCode)
    {
        SourcesLink = linkSourceCode;
    }
}