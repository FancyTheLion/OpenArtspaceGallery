using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class ImageInfoDto
{
    /// <summary>
    /// Image ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Image name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Image description
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    /// <summary>
    /// The album to which the image belongs
    /// </summary>
    [JsonPropertyName("albumId")]
    public Guid AlbumId { get; set; }
    
    /// <summary>
    /// Creation time
    /// </summary>
    [JsonPropertyName("creationTime")]
    public DateTime CreationTime { get; set; }
    
    /// <summary>
    /// All resized files
    /// </summary>
    [JsonPropertyName("files")]
    public List<ImageFileInfoDto> Files { get; set; }
}