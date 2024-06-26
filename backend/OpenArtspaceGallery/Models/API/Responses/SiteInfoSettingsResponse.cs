using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class SiteInfoSettingsResponse
{
    [JsonPropertyName("sourcesLink")]
    public SiteInfoSettingsDto SiteInfoSettings { get; set; } 

    public SiteInfoSettingsResponse(SiteInfoSettingsDto siteInfoSettings)
    {
        SiteInfoSettings = siteInfoSettings;
    }
}