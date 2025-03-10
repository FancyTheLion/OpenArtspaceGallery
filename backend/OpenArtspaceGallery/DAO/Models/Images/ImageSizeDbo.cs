using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenArtspaceGallery.DAO.Models.Images;

[Table("ImagesSizes")]
public class ImageSizeDbo
{
    /// <summary>
    /// Image size id
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Size name (large, medium and others)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///  Width
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Height
    /// </summary>
    public int Height { get; set; }
}