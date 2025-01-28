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
        var trimmedName = name.Trim();

        if (!AlbumValidator.Validate(trimmedName))
        {
            throw new ArgumentException($"'{nameof(name)}' is invalid.", nameof(name));
        }
        
        ParentAlbumId = parentAlbumId;
        Name = trimmedName;
    }
}