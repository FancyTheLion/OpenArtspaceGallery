using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.Responses.Files;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class FilesClient: IFilesClient
{
    
    private readonly HttpClient _httpClient;
    
    public FilesClient
    (
        HttpClient httpClient
    )
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5271");
    }
    
    public async Task<UploadFileResponse> UploadAsync(string filename, string mimeType, byte[] content)
    {
        var streamContent = new StreamContent(new MemoryStream(content));
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
        
        using var requestContent = new MultipartFormDataContent
        {
            {
                streamContent,
                "file",
                filename
            }
        };
        
        using var response = await _httpClient.PostAsync("/api/Files/Upload", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }
        
        var json = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<UploadFileResponse>(json);
    }
    
}