using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

/// <summary>
/// DTO for initial image loading via multipart/form-data. Contains basic fields: name,
///  description, etc., available before creating the Image
/// </summary>
public class ImageForUploadDto
{
    /// <summary>
    /// Image name 
    /// </summary>
    [JsonPropertyName("name")]
    public string Name  { get; set; }
    
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
}