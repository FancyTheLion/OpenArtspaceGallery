using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

public class BackendVersionDto
{
    /// <summary>
    ///  Backend version
    /// </summary>
    [JsonPropertyName("backendVersion")]
    public string Version { get; private set; }
    
    public BackendVersionDto(string version)
    {
        Version = version; // Add validation
    }
}
