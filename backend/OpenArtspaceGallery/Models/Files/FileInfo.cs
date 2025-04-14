namespace OpenArtspaceGallery.Models.Files;

/// <summary>
/// File info model
/// </summary>
public class FileInfo
{
    /// <summary>
    /// File ID
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Filename
    /// </summary>
    public string Name { get; private set; }
    
    public FileInfo
    (
        Guid id,
        string name
    )
    {
        Id = id;
        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("File name must be populated!", nameof(name));
        }
        Name = name;
    }
}