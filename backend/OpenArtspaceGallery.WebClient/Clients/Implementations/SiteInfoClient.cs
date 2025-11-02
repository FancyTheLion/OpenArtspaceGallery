using System.Text.Json;
using OpenArtspaceGallery.Models.API.Responses.SiteInfo;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class SiteInfoClient : ClientBase, ISiteInfoClient
{
    public SiteInfoClient
    (
        HttpClient httpClient,
        Uri baseAddress
    ) : base(httpClient, baseAddress)
    {
    }
    
    public async Task<BackendVersionResponse> GetBackendVersionAsync()
    {
        var response = await _httpClient.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }
        
        return JsonSerializer.Deserialize<BackendVersionResponse>(await response.Content.ReadAsStringAsync());
    }
}