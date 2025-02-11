using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;

namespace OpenArtspaceGallery.Models.API.Responses.Albums;

public class AlbumHierarchyResponse
{
    /// <summary>
    /// Album hierarchy
    /// </summary>
    [JsonPropertyName("albumHierarchy")]
    public IReadOnlyCollection<AlbumInHierarchyDto> AlbumHierarchy { get; private set; }
    
    public AlbumHierarchyResponse
    (
        IReadOnlyCollection<AlbumInHierarchyDto> albumHierarchy
    )
    {
        AlbumHierarchy = albumHierarchy ?? throw new ArgumentNullException(nameof(albumHierarchy), "Album hierarchy collection can't be null.");
    }
}