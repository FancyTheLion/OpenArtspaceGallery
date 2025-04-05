using System.Text.Json.Serialization;
using OpenArtspaceGallery.DAO.Models.FilesTypes;

namespace OpenArtspaceGallery.Models.API.DTOs.Files;

public class FileDownloadDto
{
    /// <summary>
    ///  File ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// File content
    /// </summary>
    [JsonPropertyName("content")]
    public byte[] Content { get; set; }
    
    /// <summary>
    /// File type
    /// </summary>
    [JsonPropertyName("type")]
    public FileTypeDbo Type { get; set; }

    /// <summary>
    /// The file name that was given by the user (before uploading to the gallery)
    /// </summary>
    [JsonPropertyName("originalName")]
    public string OriginalName { get; set; }
    
    /// <summary>
    /// Relative path in file storage
    /// </summary>
    [JsonPropertyName("storagePath")]
    public string StoragePath { get; set; }

    /// <summary>
    /// SHA-512 of file content, for use as ETag
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    /// <summary>
    /// Last modification time
    /// </summary>
    [JsonPropertyName("lastModificationTime")]
    public DateTime LastModificationTime { get; set; }
}