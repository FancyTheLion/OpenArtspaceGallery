using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class AlbumDto
{
    /// <summary>
    /// Album ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Parent album
    /// </summary>
    [JsonPropertyName("parent")]
    public Guid? Parent { get; private set; }
    
    /// <summary>
    /// Album name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }

    /// <summary>
    /// Album data create
    /// </summary>
    [JsonPropertyName("createData")]
    public DateTime CreateDate { get; private set; }

    public AlbumDto
    (
        Guid id,
        Guid? parent, 
        string name,
        DateTime createDate
    )
    {
        Id = id;
        Parent = parent;
        Name = name;
        CreateDate = createDate;
    }
}