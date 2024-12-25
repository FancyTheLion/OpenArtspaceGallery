using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class UpdateImageSizeRequest
{
    /// <summary>
    /// Image size to update by name, width and height
    /// </summary>
    [JsonPropertyName("imageSize")]
    public UpdateImageSizeDto ImageSize { get; set; }
}