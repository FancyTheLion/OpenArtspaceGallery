using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImageSizeResponse
{
    /// <summary>
    /// Image size response not for collection
    /// </summary>
    [JsonPropertyName("imageSize")]
    public ImageSizeDto ImageSize { get; private set; }

    public ImageSizeResponse
    (
        ImageSizeDto imageSize
    )
    {
        ImageSize = imageSize ?? throw new ArgumentNullException(nameof(imageSize), "Image size can't be null.");
    }
}