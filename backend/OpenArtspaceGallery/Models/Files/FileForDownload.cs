using System.Text.Json.Serialization;
using OpenArtspaceGallery.DAO.Models.FilesTypes;

namespace OpenArtspaceGallery.Models.Files;

public class FileForDownload
{
    /// <summary>
    /// File content
    /// </summary>
    public byte[] Content { get; set; }
    
    /// <summary>
    /// File type
    /// </summary>
    public FileType Type { get; set; }

    /// <summary>
    /// The file name that was given by the user (before uploading to the gallery)
    /// </summary>
    public string OriginalName { get; set; }
    
    /// <summary>
    /// SHA-512 of file content, for use as ETag
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// Last modification time
    /// </summary>
    public DateTime LastModificationTime { get; set; }
}