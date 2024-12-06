using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImageSizeDimensionsExistenceResponse
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("existence")]
    public ExistenceDto DimensionsExistence { get; set; }
    
    public ImageSizeDimensionsExistenceResponse
    (
        ExistenceDto dimensionsExistence
    )
    {
        DimensionsExistence = dimensionsExistence ?? throw new ArgumentNullException(nameof(dimensionsExistence), "Image size Dimensions can't be null.");
    }
}