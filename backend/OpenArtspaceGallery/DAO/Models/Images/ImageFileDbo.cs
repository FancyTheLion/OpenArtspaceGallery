using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Models.Images;

public class ImageFileDbo
{
    /// <summary>
    /// ID of the record in the link table
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Specifying that the foreign key is associated with ImageSizeId
    /// </summary>
    public ImageSizeDbo Size { get; set; }

    /// <summary>
    /// Specifying that the foreign key is associated with File
    /// </summary>
    public FileDbo File { get; set; }
    
    /// <summary>
    /// This image file belongs to this Image
    /// </summary>
    public ImageDbo Image { get; set; }
}