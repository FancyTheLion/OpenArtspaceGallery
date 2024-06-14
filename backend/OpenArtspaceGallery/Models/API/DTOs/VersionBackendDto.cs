using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class VersionBackendDto
{
    /// <summary>
    ///  Backend version
    /// </summary>
    [JsonPropertyName("versionBackend")]
    public string Version { get; private set; }
    
    public VersionBackendDto(string version)
    {
        Version = version;
    }
}
