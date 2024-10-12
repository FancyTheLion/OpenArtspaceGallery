using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenArtspaceGallery.DAO.Models.FilesTypes;

namespace OpenArtspaceGallery.DAO.Models.Files;

public class FileDbo
{
    /// <summary>
    ///  File ID
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Specifying that the foreign key is associated with FileType
    /// </summary>
    public FileTypeDbo Type { get; set; }

    /// <summary>
    /// The file name that was given by the user (before uploading to the gallery)
    /// </summary>
    public string OriginalName { get; set; }

    /// <summary>
    /// SHA-512 of file content, for use as ETag
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// File upload date
    /// </summary>
    public DateTime UploadTime { get; set; }
}