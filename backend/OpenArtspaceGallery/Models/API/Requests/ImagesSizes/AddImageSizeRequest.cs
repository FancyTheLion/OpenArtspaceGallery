using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class AddImageSizeRequest
{
    /// <summary>
    /// New Image Size Entry
    /// </summary>
    [JsonPropertyName("imageSize")]
    public AddImageSizeDto AddImageSize { get; set; }
}