using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.Images;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

/// <summary>
/// DTO for creating a record in the Images tabl.
/// Takes the GUID of the uploaded file and adds the album, name, and description
/// </summary>
public class AddImageDto : ImageDto
{
    /// <summary>
    /// Source file id
    /// </summary>
    [JsonPropertyName("sourceFileId")]
    public Guid SourceFileId { get; set; }
}