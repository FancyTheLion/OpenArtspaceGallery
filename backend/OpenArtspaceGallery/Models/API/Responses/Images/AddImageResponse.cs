using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Responses.Images;

public class AddImageResponse
{
    /// <summary>
    /// Added image
    /// </summary>
    [JsonPropertyName("addedImage")]
    public ImageDto Image { get; set; }
    
    public AddImageResponse
    (
        ImageDto image
    )
    {
        Image = image ?? throw new ArgumentNullException(nameof(image), "Image can't be null.");
    }
}