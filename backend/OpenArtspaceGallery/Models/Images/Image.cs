using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.Images;

public class Image
{
    /// <summary>
    /// Guid id
    /// </summary>
    public Guid Id { get; set; }
    
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

    public ImageDto ToDto()
    {
        return new ImageDto()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            AlbumId = AlbumId,
            CreationTime = CreationTime
        };
    }

    public static Image FromDbo
    (
        ImageDbo? image
    )
    {
        if (image == null)
        {
            return null;
        }
        
        if (image.Album == null)
        {
            throw new InvalidOperationException("Album is null");
        }

        return new Image
        (
            image.Id,
            image.Name,
            image.Description,
            image.Album.Id,
            image.CreationTime
        );
    }
}