using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class AlbumsSettings
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("AlbumMaxNameLength")]
    public string AlbumMaxNameLength { get; set; }
}