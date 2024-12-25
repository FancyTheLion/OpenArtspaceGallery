using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class UpdateImageSizeResponse
{
    /// <summary>
    /// Image size to update by name, width, height
    /// </summary>
    [JsonPropertyName("updateImageSize")]
    public UpdateImageSizeDto UpdateImageSize { get; private set; }
    
    public UpdateImageSizeResponse
    (
        UpdateImageSizeDto updateImageSize
    )
    {
        UpdateImageSize = updateImageSize ?? throw new ArgumentNullException(nameof(updateImageSize), "Image size are required and must not be null.");
    }
}