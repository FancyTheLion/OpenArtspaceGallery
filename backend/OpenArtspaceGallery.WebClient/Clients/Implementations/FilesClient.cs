using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.Responses.Files;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.WebClient.Clients.Implementations;

public class FilesClient: ClientBase, IFilesClient
{
    public FilesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<UploadFileResponse> UploadAsync(string filename, string mimeType, byte[] content)
    {
        HttpResponseMessage response;

        using var streamContent = new StreamContent(new MemoryStream(content));
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

        using var requestContent = new MultipartFormDataContent()
        {
            {
                streamContent, "file", filename
            }
        };
        
        response = await _httpClient.PostAsync("/api/Files/Upload", requestContent);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }
        
        return JsonSerializer.Deserialize<UploadFileResponse>(await response.Content.ReadAsStringAsync());
    }
    
}