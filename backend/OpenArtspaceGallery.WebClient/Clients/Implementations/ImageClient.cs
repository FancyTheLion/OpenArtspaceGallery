using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.Models.API.Responses.Albums;
using OpenArtspaceGallery.Models.API.Responses.Images;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class ImageClient : IImageClient
{
    private readonly HttpClient _httpClient;
    
    public ImageClient
    (
        HttpClient httpClient
    )
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5271");
    }
    
    public async Task<AddImageResponse> AddImageAsync(AddImageRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Images/Add", request);
        
        if (request == null)
        {
            throw new InvalidOperationException("Album request mustn't be null.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }
        
        return JsonSerializer.Deserialize<AddImageResponse>(await response.Content.ReadAsStringAsync());
    }
}