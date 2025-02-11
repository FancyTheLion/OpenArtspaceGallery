using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

public class AlbumExistenceResponse
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("existence")]
    public ExistenceDto Existence { get; private set; }
    
    public AlbumExistenceResponse
    (
        ExistenceDto existence
    )
    {
        Existence = existence ?? throw new ArgumentNullException(nameof(existence), "Existence is required and must not be null.");
    }
}