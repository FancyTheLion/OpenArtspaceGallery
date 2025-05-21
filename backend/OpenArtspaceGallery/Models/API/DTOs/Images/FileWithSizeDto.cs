using System.Text.Json.Serialization;

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
        Id = id;
        SizeId = sizeId;
    }
}