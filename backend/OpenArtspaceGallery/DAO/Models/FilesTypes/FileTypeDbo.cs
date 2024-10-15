using System.ComponentModel.DataAnnotations;

namespace OpenArtspaceGallery.DAO.Models.FilesTypes;

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