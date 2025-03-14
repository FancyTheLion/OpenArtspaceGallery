using System.Text.Json;
using Microsoft.AspNetCore.Http;
using OpenArtspaceGallery.Models.API.Responses.Files;

namespace OpenArtspaceGallery.Tests.Files;

public class FilesTests : IClassFixture<TestsFactory<Program>>
{
    private readonly TestsFactory<Program> _factory;

    public FilesTests(TestsFactory<Program> factory)
    {
        _factory = factory;
    }
    
    /*[Fact]
    public async Task UploadAsync_ValidPngFile_ReturnsOkResponse()
    {
        using var fileStream = new MemoryStream(GetPngTestFileBytes());
        var formFile = new FormFile(fileStream, 0, fileStream.Length, "file", "test-image.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };

        using var multipartContent = new MultipartFormDataContent
        {
            { new StreamContent(fileStream), "file", "test-image.png" }
        };
        
        var response = await _factory.HttpClient.PostAsync("api/Files/Upload", multipartContent);
        
        response.EnsureSuccessStatusCode();
    
        var responseData = JsonSerializer.Deserialize<UploadFileResponse>(await response.Content.ReadAsStringAsync());
    
        Assert.NotNull(responseData);
        Assert.NotEqual(Guid.Empty, responseData.FileInfo.Id);
    }

    private byte[] GetPngTestFileBytes()
    {
        return Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/wcAAwAB/ep9MwAAAABJRU5ErkJggg==");
    }*/
}