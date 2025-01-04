using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class ImageSizeExistenceRequest
{
    /// <summary>
    /// Image size
    /// </summary>
    [JsonPropertyName("imageSize")]
    public ImageSizeBaseDto ImageSize { get; set; }
}