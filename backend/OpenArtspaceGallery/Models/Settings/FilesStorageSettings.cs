using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class FilesStorageSettings
{
    /// <summary>
    /// Storage root path
    /// </summary>
    [JsonPropertyName("RootPath")]
    public string RootPath { get; set; }
    
    /// <summary>
    /// File max size
    /// </summary>
    [JsonPropertyName("MaxSize")]
    public int MaxSize { get; set; }
}