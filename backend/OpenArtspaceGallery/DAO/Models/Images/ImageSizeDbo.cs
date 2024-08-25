using System.ComponentModel.DataAnnotations;

namespace OpenArtspaceGallery.DAO.Models.Images;

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
    public String Name { get; set; }

    /// <summary>
    ///  Image size (Width)
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Image size(Height)
    /// </summary>
    public int Height { get; set; }
}