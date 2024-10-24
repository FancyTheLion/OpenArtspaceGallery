using OpenArtspaceGallery.Helpers.Validators;

namespace OpenArtspaceGallery.Models;

public class NewAlbum
{
    /// <summary>
    /// New album name
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Parent album ID
    /// </summary>
    public Guid? ParentAlbumId { get; private set; }

    public NewAlbum
    (        
        Guid? parentAlbumId,
        string name
    )
    {
        AlbumValidator.Validate(name);
        
        ParentAlbumId = parentAlbumId;
        Name = name;
    }
}