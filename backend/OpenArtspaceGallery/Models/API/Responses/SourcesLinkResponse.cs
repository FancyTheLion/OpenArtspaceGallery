using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class SourcesLinkResponse
{
    [JsonPropertyName("sourcesLink")]
    public SourcesLinkDto SourcesLink { get; set; } 

    public SourcesLinkResponse
    (
        SourcesLinkDto sourcesLink
    )
    {
        SourcesLink = sourcesLink;
    }
}