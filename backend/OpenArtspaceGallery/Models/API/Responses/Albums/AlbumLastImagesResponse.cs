using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

public class AlbumLastImagesResponse
{
    /// <summary>
    /// Last images in album
    /// </summary>
    [JsonPropertyName("lastImages")]
    public IReadOnlyCollection<ImageWithThumbnailDto> LastImages { get; private set; }

    public AlbumLastImagesResponse(IReadOnlyCollection<ImageWithThumbnailDto> images)
    {
        LastImages = images ?? throw new ArgumentNullException(nameof(images));
    }
}