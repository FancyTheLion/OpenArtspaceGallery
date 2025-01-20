using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class ImageSizeNameExistenceRequest
{
    [JsonPropertyName("imageSize")]
    public ImageSizeExistenceByNameDto ImageSizeExistenceByName { get; set; }
}