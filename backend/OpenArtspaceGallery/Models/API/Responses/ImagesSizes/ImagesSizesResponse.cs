using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImagesSizesResponse
{
    /// <summary>
    /// Images sizes
    /// </summary>
    [JsonPropertyName("imagesSizes")]
    public IReadOnlyCollection<ImageSizeDto> ImagesSizes { get; private set; }

    public ImagesSizesResponse
    (
        IReadOnlyCollection<ImageSizeDto> imagesSizes
    )
    {
        ImagesSizes = imagesSizes ?? throw new ArgumentNullException(nameof(imagesSizes), "Images sizes collection can't be null.");
    }
}