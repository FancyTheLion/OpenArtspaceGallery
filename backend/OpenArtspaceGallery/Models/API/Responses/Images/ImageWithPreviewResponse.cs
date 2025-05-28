using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Responses.Images;

public class ImageWithPreviewResponse
{
    /// <summary>
    /// Image with preview
    /// </summary>
    [JsonPropertyName("images")]
    public IReadOnlyCollection<ImageWithThumbnailDto> Images { get; private set; }
    
    public ImageWithPreviewResponse(IReadOnlyCollection<ImageWithThumbnailDto> imageWithPreview)
    {
        Images = imageWithPreview ?? throw new ArgumentNullException(nameof(imageWithPreview));
    }
}