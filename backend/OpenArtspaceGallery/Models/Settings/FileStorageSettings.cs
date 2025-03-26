using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class FileStorageSettings
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("RootPath")]
    public string RootPath { get; set; }
}