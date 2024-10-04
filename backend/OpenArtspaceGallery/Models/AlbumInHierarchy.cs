using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models;

public class AlbumInHierarchy
{
    /// <summary>
    /// Album ID
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    public string Name { get; private set; }
    
    public AlbumInHierarchy
    (
        Guid id,
        string name
    )
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name), "Name mustn't be null!");
    }

    public AlbumInHierarchyDto ToDto()
    {
        return new AlbumInHierarchyDto(Id, Name);
    }
}