namespace OpenArtspaceGallery.Models;

public class NewAlbum
{
    /// <summary>
    /// New album name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Parent album ID
    /// </summary>
    public Guid? ParentAlbumId { get; set; }

    public NewAlbum
    (        
        Guid? parentAlbumId,
        string name
    )
    {
        ParentAlbumId = parentAlbumId;
        Name = name;
    }
    
    //TODO: hello! I'm test commit, i love you!
}