using OpenArtspaceGallery.Helpers.Validators;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models;

public class Album
{
    /// <summary>
    /// Album ID
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Parent album
    /// </summary>
    public Guid? Parent { get; private set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Album creation time
    /// </summary>
    public DateTime CreationTime { get; private set; }

    public Album
    (
        Guid id,
        Guid? parent, 
        string name,
        DateTime creationTime
    )
    {
        AlbumValidator.Validate(name);
        
        Id = id;
        Parent = parent;
        Name = name;
        CreationTime = creationTime;
    }

    public AlbumDto ToDto()
    {
        return new AlbumDto
        (
            Id,
            Parent,
            Name,
            CreationTime
        );
    }
}