using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Responses.Albums;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class AlbumsClient : ClientBase, IAlbumsClient
{
    public AlbumsClient
    (
        HttpClient httpClient,
        Uri baseAddress
    ) : base(httpClient, baseAddress)
    {
    }
    
    public async Task<NewAlbumResponse> CreateAlbumAsync(NewAlbumRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Albums/New", request);
        
        if (request == null)
        {
            throw new InvalidOperationException("Album request mustn't be null.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }
        
        return JsonSerializer.Deserialize<NewAlbumResponse>(await response.Content.ReadAsStringAsync());
    }
}