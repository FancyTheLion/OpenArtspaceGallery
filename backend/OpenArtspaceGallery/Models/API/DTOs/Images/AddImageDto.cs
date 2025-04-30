using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.Images;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class AddImageDto : ImageDto
{
    /// <summary>
    /// Source file id
    /// </summary>
    [JsonPropertyName("sourceFileId")]
    public Guid SourceFileId { get; set; }
}