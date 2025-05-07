using OpenArtspaceGallery.DAO.Models.Files;

namespace OpenArtspaceGallery.Models.Files;

/// <summary>
/// File metadata
/// </summary>
public class FileMetadata
{
    /// <summary>
    ///  File ID
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Specifying that the foreign key is associated with FileType
    /// </summary>
    public FileType Type { get; private set; }

    /// <summary>
    /// The file name that was given by the user (before uploading to the gallery)
    /// </summary>
    public string OriginalName { get; private set; }
    
    /// <summary>
    /// Relative path in file storage
    /// </summary>
    public string StoragePath { get; private set; }

    /// <summary>
    /// SHA-512 of file content, for use as ETag
    /// </summary>
    public string Hash { get; private set; }

    /// <summary>
    /// Last modification time
    /// </summary>
    public DateTime LastModificationTime { get; private set; }

    public FileMetadata
    (
        Guid id,
        FileType type,
        string originalName,
        string storagePath,
        string hash,
        DateTime lastModificationTime
    )
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        if (string.IsNullOrWhiteSpace(originalName))
        {
            throw new ArgumentException("Filename mustn't be empty!", nameof(originalName));
        }
        
        if (string.IsNullOrWhiteSpace(storagePath))
        {
            throw new ArgumentException("Storage path mustn't be empty!", nameof(storagePath));
        }
        
        Id = id;
        Type = type;
        OriginalName = originalName;
        StoragePath = storagePath;
        Hash = hash;
        LastModificationTime = lastModificationTime;
    }

    public static FileMetadata FromDbo(FileDbo dbo)
    {
        return new FileMetadata
        (
            dbo.Id,
            FileType.FromDbo(dbo.Type),
            dbo.OriginalName,
            dbo.StoragePath,
            dbo.Hash,
            dbo.LastModificationTime
        );
    }
}