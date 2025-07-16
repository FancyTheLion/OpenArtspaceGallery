using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

public class AlbumLastImageResponse
{
    /// <summary>
    /// Last images in album
    /// </summary>
    [JsonPropertyName("lastImage")]
    public IReadOnlyCollection<ImageWithThumbnailDto> LastImages { get; private set; }

    public AlbumLastImageResponse(IReadOnlyCollection<ImageWithThumbnailDto> images)
    {
        LastImages = images ?? throw new ArgumentNullException(nameof(images));
    }
}