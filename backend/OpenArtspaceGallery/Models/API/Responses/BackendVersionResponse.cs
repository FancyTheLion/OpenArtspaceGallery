using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class BackendVersionResponse
{
    /// <summary>
    /// Backend version
    /// </summary>
    [JsonPropertyName("backendVersion")]
    public BackendVersionDto BackendVersion { get; private set; } 

    public BackendVersionResponse
    (
        BackendVersionDto backendVersion
    )
    {
        BackendVersion = backendVersion ?? throw new ArgumentNullException(nameof(backendVersion), "Album list can't be null.");
    }
}