using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.SiteInfo;

namespace OpenArtspaceGallery.Models.API.Responses.SiteInfo;

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
        BackendVersion = backendVersion ?? throw new ArgumentNullException(nameof(backendVersion), "Backend version can't be null.");
    }
}