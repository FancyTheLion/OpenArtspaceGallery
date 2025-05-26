using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.Images;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class ImageWithPreviewDto : ImageDto
{
    /// <summary>
    /// Thumbnail file id
    /// </summary>
    [JsonPropertyName("thumbnailId")]
    public Guid ThumbnailId { get; set; }
    
    public static ImageWithPreviewDto FromModel(Image image, Guid thumbnailId)
    {
        return new ImageWithPreviewDto
        {
            Id = image.Id,
            Name = image.Name,
            Description = image.Description,
            AlbumId = image.AlbumId,
            CreationTime = image.CreationTime,
            ThumbnailId = thumbnailId
        };
    }
}