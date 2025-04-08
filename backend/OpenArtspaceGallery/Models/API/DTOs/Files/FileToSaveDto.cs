namespace OpenArtspaceGallery.Models.API.DTOs.Files;

public class FileToSaveDto
{
    /// <summary>
    /// File id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Original name
    /// </summary>
    public string OriginalFileName { get; set; }
    
    /// <summary>
    /// File type id
    /// </summary>
    public Guid FileTypeId { get; set; }
    
    /// <summary>
    /// File path
    /// </summary>
    public string FilePath { get; set; }
    
    /// <summary>
    /// Content
    /// </summary>
    public byte[] Content { get; set; }
}