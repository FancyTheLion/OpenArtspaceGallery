using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class SourcesLinkDto
{
    /// <summary>
    ///  Source code link
    /// </summary>
    [JsonPropertyName("sourcesLink")]
    public string SourcesLink { get; private set; }
    
    public SourcesLinkDto(string sourcesLink)
    {
        if (string.IsNullOrWhiteSpace(sourcesLink))
        {
            throw new ArgumentException("Source link mustn't be null or whitespace.", nameof(sourcesLink));
        }
        
        SourcesLink = sourcesLink;
    }
}