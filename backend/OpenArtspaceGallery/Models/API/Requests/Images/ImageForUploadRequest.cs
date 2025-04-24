using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models.API.Requests.Images;

/// <summary>
/// Wrapper over ImageForUploadDto for multipart/form-data.
/// Used when uploading a file with a name, his album and description
/// </summary>
public class ImageForUploadRequest
{
    /// <summary>
    /// New image
    /// </summary>
    [JsonPropertyName("imageForUpload")]
    public ImageForUploadDto ImageForUpload { get; set; }
}