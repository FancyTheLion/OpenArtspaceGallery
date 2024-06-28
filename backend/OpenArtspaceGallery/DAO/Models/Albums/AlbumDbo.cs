using System.ComponentModel.DataAnnotations;

namespace OpenArtspaceGallery.DAO.Models.Albums;

public class AlbumDbo
{
    /// <summary>
    /// Album ID
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Album Name
    /// </summary>
    public AlbumDbo? Parent { get; set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Album data create
    /// </summary>
    public DateTime CreateDate { get; set; }
}