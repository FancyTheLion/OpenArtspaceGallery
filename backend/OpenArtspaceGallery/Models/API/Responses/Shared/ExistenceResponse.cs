using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.Shared;

public class ExistenceResponse
{
    /// <summary>
    /// Does anything exist
    /// </summary>
    [JsonPropertyName("existence")]
    public ExistenceDto Existence { get; private set; }
    
    public ExistenceResponse
    (
        ExistenceDto existence
    )
    {
        Existence = existence ?? throw new ArgumentNullException(nameof(existence), "Existence is required and must not be null.");
    }
}