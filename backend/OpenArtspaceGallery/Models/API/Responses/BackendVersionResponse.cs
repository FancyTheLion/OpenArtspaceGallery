using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class BackendVersionResponse
{
    [JsonPropertyName("backendVersion")]
    public BackendVersionDto BackendVersion { get; set; } 

    public BackendVersionResponse
    (
        BackendVersionDto backendVersion
    )
    {
        BackendVersion = backendVersion;
    }
}