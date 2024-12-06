using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class ImageSizeNameExistenceRequest
{
    /// <summary>
    /// Image size name existence
    /// </summary>
    [JsonPropertyName("name")]
    public ImageSizeNameDto ImageSizeName { get; set; }
}