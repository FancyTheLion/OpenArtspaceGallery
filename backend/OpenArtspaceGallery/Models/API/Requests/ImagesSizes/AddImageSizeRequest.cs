using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class AddImageSizeRequest
{
    /// <summary>
    /// New image size
    /// </summary>
    [JsonPropertyName("imageSize")]
    public AddImageSizeDto ImageSize { get; set; }
}