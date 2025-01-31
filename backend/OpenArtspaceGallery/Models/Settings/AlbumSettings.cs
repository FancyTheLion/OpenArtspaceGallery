using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class AlbumSettings
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("AlbumMaxNameLength")]
    public string AlbumMaxNameLength { get; set; }
}