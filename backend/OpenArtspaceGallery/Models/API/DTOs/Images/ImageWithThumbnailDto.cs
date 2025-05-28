using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.Images;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class ImageWithThumbnailDto : ImageDto
{
    /// <summary>
    /// Thumbnail file id
    /// </summary>
    [JsonPropertyName("thumbnailId")]
    public Guid ThumbnailId { get; private set; }
    
    public static ImageWithThumbnailDto FromModel(Image image, Guid thumbnailId)
    {
        return new ImageWithThumbnailDto
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