using System.Text.Json.Serialization;
using OpenArtspaceGallery.Helpers.Validators;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class FileWithSizeDto
{
    /// <summary>
    /// File id
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// File size ID
    /// </summary>
    [JsonPropertyName("sizeId")]
    public Guid SizeId { get; private set; }

    public FileWithSizeDto(Guid id, Guid sizeId)
    {
        GuidValidator.Validate(id);
        Id = id;
        
        GuidValidator.Validate(sizeId);
        SizeId = sizeId;
    }
}