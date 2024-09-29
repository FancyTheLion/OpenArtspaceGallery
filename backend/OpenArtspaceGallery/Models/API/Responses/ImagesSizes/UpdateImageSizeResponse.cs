using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class UpdateImageSizeResponse
{
    /// <summary>
    /// Update image size
    /// </summary>
    [JsonPropertyName("imageSize")]
    public ImageSizeDto ImageSize { get; private set; }

    public UpdateImageSizeResponse(ImageSizeDto imageSize)
    {
        ImageSize = imageSize ?? throw new ArgumentNullException(nameof(imageSize), "Update image size can't be null.");
    }
}