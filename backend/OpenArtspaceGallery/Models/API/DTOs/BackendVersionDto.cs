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
        if (string.IsNullOrWhiteSpace(version))
        {
            throw new ArgumentException("Backend version mustn't be null or whitespace.", nameof(version));
        }
        
        Version = version;
    }
}
