using OpenArtspaceGallery.Helpers.Validators;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.Albums;

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
        AlbumValidator.Validate(name);
            
        Id = id;
        Name = name;
    }

    public AlbumInHierarchyDto ToDto()
    {
        return new AlbumInHierarchyDto(Id, Name);
    }
}