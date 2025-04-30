using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Requests.Images;

public class AddImageRequest
{
    /// <summary>
    /// Add image
    /// </summary>
    [JsonPropertyName("image")]
    public AddImageDto Image { get; set; }
}