using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Responses.Images;

public class ImageWithPreviewResponse
{
    /// <summary>
    /// Image with preview
    /// </summary>
    [JsonPropertyName("image")]
    public ImageWithPreviewDto Image { get; private set; }
    
    public ImageWithPreviewResponse(ImageWithPreviewDto imageWithPreview)
    {
        Image = imageWithPreview ?? throw new ArgumentNullException(nameof(imageWithPreview));
    }
}