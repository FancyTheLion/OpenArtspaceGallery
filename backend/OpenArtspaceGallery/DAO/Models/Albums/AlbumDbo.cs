using System.ComponentModel.DataAnnotations;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.DAO.Models.Albums;

public class AlbumDbo
{
    /// <summary>
    /// Album ID
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Parent album
    /// </summary>
    public AlbumDbo? Parent { get; set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Album creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

    public Album ToModel()
    {
        return new Album
        (
            Id,
            Parent?.Id,
            Name,
            CreationTime
        );
    }
}