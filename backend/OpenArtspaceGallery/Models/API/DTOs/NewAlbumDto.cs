using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class NewAlbumDto
{
    /// <summary>
    /// New album name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Parent album ID
    /// </summary>
    [JsonPropertyName("parentId")]
    public Guid? ParentId { get; set; }

    public NewAlbum ToModel()
    {
        return new NewAlbum(ParentId, Name);
    }
}