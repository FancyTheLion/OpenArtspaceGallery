using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.DAO.Models.Images;

[Table("Images")]
public class ImageDbo
{
    /// <summary>
    /// Id image
    /// </summary>
    [Key]
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
    /// Image upload date
    /// </summary>
    public DateTime CreationTime { get; set; }
    
    /// <summary>
    /// Image files
    /// </summary>
    public IList<ImageFileDbo> Files { get; set; }
    
    /// <summary>
    /// Image belongs to this album
    /// </summary>
    public AlbumDbo Album { get; set; }
}