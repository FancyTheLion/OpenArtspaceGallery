using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Responses.Images;

public class ImageInfoResponse
{
    /// <summary>
    /// Image info
    /// </summary>
    [JsonPropertyName("image")]
    public ImageInfoDto Image { get; set; }

    public ImageInfoResponse(ImageInfoDto image)
    {
        Image = image ?? throw new ArgumentNullException(nameof(image));
    }
}