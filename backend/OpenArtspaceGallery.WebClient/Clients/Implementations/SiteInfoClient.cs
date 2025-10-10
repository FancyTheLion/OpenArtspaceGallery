using System.Text.Json;
using OpenArtspaceGallery.Models.API.Responses.SiteInfo;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class SiteInfoClient : ISiteInfoClient
{
    private readonly HttpClient _httpClient;

    public SiteInfoClient
    (
        HttpClient httpClient
    )
    {
        _httpClient = httpClient;
        
        // TODO: Use parameters from outside
        _httpClient.BaseAddress = new Uri("http://localhost:5271");
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