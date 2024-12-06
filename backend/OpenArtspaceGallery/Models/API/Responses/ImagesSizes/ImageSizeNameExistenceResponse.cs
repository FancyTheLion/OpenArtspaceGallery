using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImageSizeNameExistenceResponse
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("existence")]
    public ExistenceDto Existence { get; set; }
    
    public ImageSizeNameExistenceResponse
    (
        ExistenceDto existence
    )
    {
        Existence = existence ?? throw new ArgumentNullException(nameof(existence), "Image size can't be null.");
    }
}