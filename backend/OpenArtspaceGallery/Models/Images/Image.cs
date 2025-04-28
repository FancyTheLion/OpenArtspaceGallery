using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.Models.Images;

public class Image
{
    public Guid Id { get; }
    
    /// <summary>
    /// Image name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Image description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// The album to which the image belongs
    /// </summary>
    public Guid AlbumId { get; set; }
    
    /// <summary>
    /// Data creation time
    /// </summary>
    public DateTime CreationTime { get; set; }
    
    public Image(Guid id, string name, string description, Guid albumId, DateTime creationTime)
    {
        Id = id;
        Name = name;
        Description = description;
        AlbumId = albumId;
        CreationTime = creationTime;
    }

    public static Image FromDbo(ImageDbo image)
    {
        return new Image(
            image.Id,
            image.Name,
            image.Description,
            image.Album.Id,
            image.CreationTime
        );
    }
}