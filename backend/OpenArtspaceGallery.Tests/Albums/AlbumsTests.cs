using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;

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
        var dto = new NewAlbumDto
        {
            Name = "AlbumToBeCreated",
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
        
        // A ModelState failure returns to Page (200-OK) and doesn't redirect.
        //response.EnsureSuccessStatusCode();
        //Assert.Null(response.Headers.Location?.OriginalString);
    }
    
}