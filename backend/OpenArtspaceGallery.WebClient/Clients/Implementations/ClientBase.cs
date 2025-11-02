using Microsoft.Extensions.Configuration;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public abstract class ClientBase
{
    protected readonly HttpClient _httpClient;
    
    protected ClientBase
    (
        HttpClient httpClient,
        Uri baseAddress
    )
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }
}