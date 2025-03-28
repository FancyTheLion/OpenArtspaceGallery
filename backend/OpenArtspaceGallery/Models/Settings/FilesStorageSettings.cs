using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.Settings;

public class FilesStorageSettings
{
    /// <summary>
    /// Storage root path
    /// </summary>
    [JsonPropertyName("RootPath")]
    public string RootPath { get; set; }
}