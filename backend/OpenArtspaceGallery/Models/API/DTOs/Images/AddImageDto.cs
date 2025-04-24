namespace OpenArtspaceGallery.Models.API.DTOs.Images;

/// <summary>
/// DTO for creating a record in the Images tabl.
/// Takes the GUID of the uploaded file and adds the album, name, and description
/// </summary>
public class AddImageDto
{
    /// <summary>
    /// Image name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Image description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// The album to which the image belongs
    /// </summary>
    public Guid AlbumId { get; set; }
    
    /// <summary>
    /// Source file id
    /// </summary>
    public Guid SourceFileId { get; set; }
}