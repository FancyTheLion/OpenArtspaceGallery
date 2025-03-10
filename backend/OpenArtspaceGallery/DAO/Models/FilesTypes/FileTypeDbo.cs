using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenArtspaceGallery.DAO.Models.FilesTypes;

[Table("FilesTypes")]
public class FileTypeDbo
{
    /// <summary>
    /// File type GUID
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Mime file type
    /// </summary>
    public string MimeType  { get; set; }
    
    /// <summary>
    /// File Description
    /// </summary>
    public string Description { get; set; }
}