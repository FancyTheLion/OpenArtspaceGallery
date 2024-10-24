using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

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