using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.DAO.Models.Images;

public class ImageDbo
{
    /// <summary>
    /// Id image
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Specifying that the foreign key is associated with Album
    /// </summary>
    public AlbumDbo Album { get; set; }
    
    /// <summary>
    /// Image name
    /// </summary>
    public String Name { get; set; }

    /// <summary>
    /// Image Description
    /// </summary>
    public String Description { get; set; }

    /// <summary>
    /// Image upload date
    /// </summary>
    public DateTime CreationTime { get; set; }
}