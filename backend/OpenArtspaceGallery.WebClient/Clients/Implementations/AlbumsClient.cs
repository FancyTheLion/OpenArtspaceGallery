using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Responses.Albums;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class AlbumsClient : IAlbumsClient
{
    private readonly HttpClient _httpClient;
    
    public AlbumsClient
    (
        HttpClient httpClient
    )
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5271");
    }
    
    public async Task<NewAlbumResponse> CreateAlbumAsync(NewAlbumRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Albums/New", request);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }
        
        return JsonSerializer.Deserialize<NewAlbumResponse>(await response.Content.ReadAsStringAsync());
    }
}