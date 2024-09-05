using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

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
        ImageSize = imagesSizes ?? throw new ArgumentNullException(nameof(imagesSizes), "Images sizes list can't be null.");
    }
}