using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class AnotherImageSizeNameExistenceRequest
{
    [JsonPropertyName("imageSize")]
    public AnotherImageSizeExistenceByNameDto AnotherImageSizeExistenceByName { get; set; }
}