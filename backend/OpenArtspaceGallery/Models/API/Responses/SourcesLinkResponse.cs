using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

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
        SourcesLink = sourcesLink ?? throw new ArgumentNullException(nameof(sourcesLink), "Album list can't be null.");
    }
}