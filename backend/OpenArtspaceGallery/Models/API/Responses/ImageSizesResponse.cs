using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class ImageSizesResponse
{
    /// <summary>
    /// Images sizes
    /// </summary>
    [JsonPropertyName("imageSizes")]
    public IReadOnlyCollection<ImageSizeDto> ImagesSizes { get; private set; }

    public ImageSizesResponse
    (
        IReadOnlyCollection<ImageSizeDto> imagesSizes
    )
    {
        ImagesSizes = imagesSizes ?? throw new ArgumentNullException(nameof(imagesSizes), "Images sizes list can't be null.");
    }
}