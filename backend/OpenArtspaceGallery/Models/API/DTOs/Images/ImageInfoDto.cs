using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.Images;
using OpenArtspaceGallery.Models.ImagesSizes;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Models.API.DTOs.Images;

public class ImageInfoDto
{
    /// <summary>
    /// Image ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Image name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }
    
    /// <summary>
    /// Image description
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; private set; }
    
    /// <summary>
    /// The album to which the image belongs
    /// </summary>
    [JsonPropertyName("albumId")]
    public Guid AlbumId { get; private set; }
    
    /// <summary>
    /// Creation time
    /// </summary>
    [JsonPropertyName("creationTime")]
    public DateTime CreationTime { get; private set; }
    
    /// <summary>
    /// All resized files
    /// </summary>
    [JsonPropertyName("files")]
    public List<FileWithSizeDto> Files { get; private set; }

    public ImageInfoDto(Image image, IReadOnlyDictionary<Guid, Guid> imageFiles)
    {
        _ = image ?? throw new ArgumentNullException(nameof(image), "Image must not be null!");
        
        Id = image.Id;
        Name = image.Name;
        Description = image.Description;
        AlbumId = image.AlbumId;
        CreationTime = image.CreationTime;

        Files = imageFiles
            .Select(imageFile => new FileWithSizeDto(imageFile.Value, imageFile.Key))
            .ToList();
    }
}