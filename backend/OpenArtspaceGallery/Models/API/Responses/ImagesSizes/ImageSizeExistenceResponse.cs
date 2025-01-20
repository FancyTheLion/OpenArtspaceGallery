using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImageSizeExistenceResponse
{
    /// <summary>
    /// Does image size exist
    /// </summary>
    [JsonPropertyName("imageSizeExistence")]
    public ExistenceDto ImageSizeExistence { get; private set; }
    
    public ImageSizeExistenceResponse
    (
        ExistenceDto imageSizeExistence
    )
    {
        ImageSizeExistence = imageSizeExistence ?? throw new ArgumentNullException(nameof(imageSizeExistence), "Image size are required and must not be null.");
    }
}