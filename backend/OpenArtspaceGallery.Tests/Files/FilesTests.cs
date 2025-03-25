using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.Models.API.Responses.Files;

namespace OpenArtspaceGallery.Tests.Files;

public class FilesTests : IClassFixture<TestsFactory<Program>>
{
    private readonly TestsFactory<Program> _factory;
    private readonly IFilesDao _filesDao;
    
    public FilesTests(TestsFactory<Program> factory, IFilesDao filesDao)
    {
        _factory = factory;
        _filesDao = filesDao;
    }
    
    [Fact]
    public async Task UploadAsync_ValidPngFile_ReturnsOkWithFileId()
    {
        await using var stream = new MemoryStream(GetFakePngBytes());
    
        var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "\"file\"",
            FileName = "\"testimage.png\""
        };

        content.Add(fileContent, "file", "testimage.png");

        var response = await _factory.HttpClient.PostAsync("/api/Files/Upload", content);

        response.EnsureSuccessStatusCode(); 
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<UploadFileResponse>(responseString, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(responseObject);
        Assert.False(responseObject.FileInfo.Id == Guid.Empty);
    }
    
    private static byte[] GetFakePngBytes()
    {
        return new byte[]
        {
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 
            0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52, 
            0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 
            0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0x15, 0xC4, 
            0x89, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41, 
            0x54, 0x78, 0x9C, 0x63, 0x60, 0x00, 0x02, 0x00, 
            0x00, 0x05, 0x00, 0x01, 0x0D, 0x0A, 0x2D, 0xB4, 
            0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 
            0xAE, 0x42, 0x60, 0x82
        };
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