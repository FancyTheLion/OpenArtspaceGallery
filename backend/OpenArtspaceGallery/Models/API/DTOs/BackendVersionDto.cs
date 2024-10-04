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
        Version = version ?? throw new ArgumentNullException(nameof(version), "Name mustn't be null!");
    }
}
