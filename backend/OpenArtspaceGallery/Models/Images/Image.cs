namespace OpenArtspaceGallery.Models.Images;

public class Image
{
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
}