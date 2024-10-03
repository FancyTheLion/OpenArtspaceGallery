using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class SourcesLinkResponse
{
    // TODO: Add comment
    [JsonPropertyName("sourcesLink")]
    public SourcesLinkDto SourcesLink { get; set; } // TODO: Private set 

    public SourcesLinkResponse
    (
        SourcesLinkDto sourcesLink
    )
    {
        SourcesLink = sourcesLink; // TODO: Add validation
    }
}