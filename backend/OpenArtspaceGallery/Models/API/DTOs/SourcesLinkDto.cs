using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class SourcesLinkDto
{
    /// <summary>
    ///  Source code link
    /// </summary>
    [JsonPropertyName("sourcesLink")]
    public string SourcesSourcesLink { get; private set; }
    
    public SourcesLinkDto(string sourcesLink)
    {
        SourcesSourcesLink = sourcesLink;
    }
}