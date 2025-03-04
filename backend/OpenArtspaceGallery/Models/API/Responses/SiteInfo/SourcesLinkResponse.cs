using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.SiteInfo;

namespace OpenArtspaceGallery.Models.API.Responses.SiteInfo;

public class SourcesLinkResponse
{
    /// <summary>
    /// Source link
    /// </summary>
    [JsonPropertyName("sourcesLink")]
    public SourcesLinkDto SourcesLink { get; private set; } 

    public SourcesLinkResponse
    (
        SourcesLinkDto sourcesLink
    )
    {
        SourcesLink = sourcesLink ?? throw new ArgumentNullException(nameof(sourcesLink), "Sources link can't be null.");
    }
}