using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImageSizeDimensionsExistenceResponse
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("dimensionsExistence")]
    public ExistenceDto DimensionsExistence { get; private set; }
    
    public ImageSizeDimensionsExistenceResponse
    (
        ExistenceDto dimensionsExistence
    )
    {
        DimensionsExistence = dimensionsExistence ?? throw new ArgumentNullException(nameof(dimensionsExistence), "Dimensions are required and must not be null."); // TODO: Remove copypasta
    }
}