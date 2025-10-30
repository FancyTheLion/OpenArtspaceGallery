namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public abstract class ClientBase
{
    protected readonly HttpClient _httpClient;
    
    protected ClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:5271");
    }
}