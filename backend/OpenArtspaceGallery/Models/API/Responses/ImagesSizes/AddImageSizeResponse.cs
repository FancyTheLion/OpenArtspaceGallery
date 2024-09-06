using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class AddImageSizeResponse
{
    /// <summary>
    /// Image size
    /// </summary>
    [JsonPropertyName("imageSize")]
    public ImageSizeDto ImageSize { get; private set; }

    public AddImageSizeResponse
    (
        ImageSizeDto imagesSizes
    )
    {
        ImageSize = imagesSizes ?? throw new ArgumentNullException(nameof(imagesSizes), "Image size can't be null.");
    }
}