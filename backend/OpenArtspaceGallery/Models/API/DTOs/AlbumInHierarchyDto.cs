using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class AlbumInHierarchyDto
{
    /// <summary>
    /// Album ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }
    
    public AlbumInHierarchyDto
    (
        Guid id,
        string name
    )
    {
        Id = id;
        Name = name; // TODO: Add validation
    }
}