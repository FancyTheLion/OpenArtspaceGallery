using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class VersionBackendResponse
{
    [JsonPropertyName("backendVersion")]
    public VersionBackendDto BackendVersion { get; set; } 

    public VersionBackendResponse(VersionBackendDto backendVersion)
    {
        BackendVersion = backendVersion;
    }
}