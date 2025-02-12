using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Shared;

public class ExistenceDto
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("Existence")]
    public bool Exists { get; private set; }
    
    public ExistenceDto(bool exists)
    {
        Exists = exists;
    }
}