using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Responses;

namespace OpenArtspaceGallery.Tests.Albums;

public class AlbumsTests : IClassFixture<TestsFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestsFactory<Program> _factory;

    public AlbumsTests(TestsFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task Post_AddNewAlbum_ReturnsSuccess_WhenAllDataIsCorrect()
    {
        // Arrange
        string albumName = $"Test Album { Guid.NewGuid() }";
        
        var dto = new NewAlbumDto
        {
            Name = albumName,
            ParentId = null
        };

        var request = new NewAlbumRequest
        {
            AlbumToAdd = dto
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Albums/New", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseData = JsonSerializer.Deserialize<NewAlbumResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); // Did we get request?
        Assert.NotNull(responseData.NewAlbum); // Did we get DTO?
        Assert.Equal(albumName, responseData.NewAlbum.Name); // Name
        Assert.Null(responseData.NewAlbum.Parent); // Must have no parent
    }
    
}