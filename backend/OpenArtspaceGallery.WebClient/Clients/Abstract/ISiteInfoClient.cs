using OpenArtspaceGallery.Models.API.Responses.SiteInfo;

namespace OpenArtspaceGallery.WebClient.Clients.Abstract;

/// <summary>
/// Client to get information about site
/// </summary>
public interface ISiteInfoClient
{
    /// <summary>
    /// Get backend version
    /// </summary>
    Task<BackendVersionResponse> GetBackendVersionAsync();
}