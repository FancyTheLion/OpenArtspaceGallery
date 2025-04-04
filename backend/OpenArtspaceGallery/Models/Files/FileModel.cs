using OpenArtspaceGallery.DAO.Models.FilesTypes;

namespace OpenArtspaceGallery.Models.Files;

public class FileModel
{
    /// <summary>
    ///  File ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// File content
    /// </summary>
    public byte[] Content { get; set; }
    
    /// <summary>
    /// File type
    /// </summary>
    public FileTypeDbo Type { get; set; }

    /// <summary>
    /// The file name that was given by the user (before uploading to the gallery)
    /// </summary>
    public string OriginalName { get; set; }
    
    /// <summary>
    /// Relative path in file storage
    /// </summary>
    public string StoragePath { get; set; }

    /// <summary>
    /// SHA-512 of file content, for use as ETag
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// Last modification time
    /// </summary>
    public DateTime LastModificationTime { get; set; }
}