using OpenArtspaceGallery.DAO.Models.FilesTypes;

namespace OpenArtspaceGallery.Models.Files;

/// <summary>
/// Base file type
/// </summary>
public class FileType
{
    public Guid Id { get; private set; }
    public string MimeType { get; private set; }
    public string Description { get; private set; }

    public FileType(Guid id, string mimeType, string description)
    {
        Id = id;
        MimeType = mimeType;
        Description = description;
    }

    public static FileType FromDbo(FileTypeDbo dbo)
    {
        _ = dbo ?? throw new ArgumentNullException(nameof(dbo));
        
        return new FileType(dbo.Id, dbo.MimeType, dbo.Description);
    }
}