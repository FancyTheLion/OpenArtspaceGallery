using System.Text.Json.Serialization;
using OpenArtspaceGallery.Helpers.Validators;

namespace OpenArtspaceGallery.Models.API.DTOs.Albums;

public class AlbumDto
{
    /// <summary>
    /// Album ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Parent album
    /// </summary>
    [JsonPropertyName("parent")]
    public Guid? Parent { get; private set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }

    /// <summary>
    /// Album creation time
    /// </summary>
    [JsonPropertyName("creationTime")]
    public DateTime CreationTime { get; private set; }

    public AlbumDto
    (
        Guid id,
        Guid? parent, 
        string name,
        DateTime creationTime
    )
    {
        if (!AlbumValidator.Validate(name))
        {
            throw new ArgumentException($"'{nameof(name)}' is invalid.", nameof(name));
        }
        
        Id = id;
        Parent = parent;
        Name = name;
        CreationTime = creationTime;
    }
    
    
}