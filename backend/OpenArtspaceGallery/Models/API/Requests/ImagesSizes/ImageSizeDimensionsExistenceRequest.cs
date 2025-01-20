using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.ImagesSizes;

public class ImageSizeDimensionsExistenceRequest
{
    /// <summary>
    /// Image size dimensions existence
    /// </summary>
    [JsonPropertyName("dimensions")]
    public ImageSizeDimensionsDto ImageSizeDimensionsExistence { get; set; }
}