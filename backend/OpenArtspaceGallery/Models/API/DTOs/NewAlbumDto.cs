using System.Text.Json.Serialization;
using OpenArtspaceGallery.Helpers.Validators;

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

    /// <summary>
    /// Checks if new album data is valid
    /// </summary>
    /// <returns>True if data is valid</returns>
    public bool Validate()
    {
        return AlbumValidator.Validate(Name);
    }
}