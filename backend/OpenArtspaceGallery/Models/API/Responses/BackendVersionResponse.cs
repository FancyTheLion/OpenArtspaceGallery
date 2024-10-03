using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class BackendVersionResponse
{
    [JsonPropertyName("backendVersion")]
    public BackendVersionDto BackendVersion { get; set; } // TODO: Private set

    public BackendVersionResponse
    (
        BackendVersionDto backendVersion
    )
    {
        BackendVersion = backendVersion; // TODO: Add validation
    }
}