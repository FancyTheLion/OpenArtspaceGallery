using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using OpenArtspaceGallery.Controllers;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OpenArtspaceGallery.Tests.Albums;

public class AlbumsTests : IClassFixture<TestsFactory<Program>>
{
    #region Initialization
    
    private readonly TestsFactory<Program> _factory;
    private readonly Mock<IAlbumsService> _mockAlbumsService;
    
    public AlbumsTests(TestsFactory<Program> factory)
    {
        _factory = factory;
        
        _mockAlbumsService = new Mock<IAlbumsService>();
    }

#endregion

    #region Create album

     public static IEnumerable<object[]> AddNewAlbum_CheckNameTrim_DataGenerator()
    {
        var baseName = $"Megaalbum!{ Guid.NewGuid() }";
        
        return new List<object[]>
        {
            new object[] { baseName, baseName},
            new object[] { $"    { baseName }", baseName},
            new object[] { $"{ baseName }    ", baseName},
            new object[] { $"    { baseName }    ", baseName},
            new object[] { $"Prefix { baseName }", $"Prefix { baseName }"}
        };
    }
    
    [Theory]
    [MemberData(nameof(AddNewAlbum_CheckNameTrim_DataGenerator))]
    public async Task AddNewAlbum_CheckNameTrim(string givenName, string expectedName)
    {
        // Act
        var response = await CreateAlbumAsync(givenName, null);
        
        // Assert
        Assert.Equal(expectedName, response.NewAlbum.Name); // Name
        Assert.Null(response.NewAlbum.Parent); // Must have no parent
    }

    [Theory]
    [MemberData(nameof(CreateAlbumData))]
    public async Task AddNewAlbum_CheckNameCorrectness(string name, bool isMustBeSuccessful)
    {
        await CreateAlbumAsync(name, null, isMustBeSuccessful ? HttpStatusCode.OK : HttpStatusCode.BadRequest, true);
    }
    
    #endregion
    
    #region Get top level albums

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsCorrectStatusCode()
    {
        var albums = GetTestAlbums();
        
        SetupMockAlbumService(albums); 

        var controller = CreateController();
        
        var result = await controller.GetTopLevelAlbumsListAsync();
        
        var actionResult = Assert.IsType<ActionResult<AlbumsListResponse>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsEmptyList()
    {
        var albums = new List<Album>(); 

        SetupMockAlbumService(albums); 

        var controller = CreateController();
        
        var result = await controller.GetTopLevelAlbumsListAsync();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<AlbumsListResponse>(okResult.Value);
        Assert.Empty(response.Albums); 
    }

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ThrowsException()
    {
        _mockAlbumsService
            .Setup(service => service.GetChildrenAsync(null))
            .ThrowsAsync(new Exception("Test exception"));

        var controller = CreateController();

        var exception = await Assert.ThrowsAsync<Exception>(() => controller.GetTopLevelAlbumsListAsync());

        Assert.Equal("Test exception", exception.Message);
    }

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsCorrectData()
    {
        var albums = GetTestAlbums();
        
        SetupMockAlbumService(albums); 
        
        var controller = CreateController();
        
        var result = await controller.GetTopLevelAlbumsListAsync();
        
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<AlbumsListResponse>(actionResult.Value);
        
        Assert.Equal(2, response.Albums.Count);
        Assert.Equal(albums[0].Name, response.Albums.ElementAt(0).Name);
        Assert.Equal(albums[1].Name, response.Albums.ElementAt(1).Name);
    }

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsAlbums()
    {
        var client = CreateClient();
        
        var response = await client.GetAsync("/api/albums/TopLevel");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var albums = JsonConvert.DeserializeObject<AlbumsListResponse>(content);
        
        Assert.NotNull(albums);
    }

    
    //If a test is run in a batch of tests, it sees that the list is not empty. If it is the only one running, the list is empty.
    /*[Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsExpectedDataStructure()
    {
        var client = CreateClient();
        
        var response = await client.GetAsync("/api/albums/TopLevel");
        
        response.EnsureSuccessStatusCode();
        
        var albumsList = JsonSerializer.Deserialize<AlbumsListResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(albumsList);
        Assert.Empty(albumsList.Albums);
    }*/

    #endregion
    
    #region Get children albums list
    
    /*[Fact]
    public async Task GetChildrenAlbumsListAsync_ReturnsChildren_WhenAlbumExists()
    {
        var client = _factory.CreateClient();
        var existingAlbumId = Guid.NewGuid();


        var response = await client.GetAsync($"/api/Albums/ChildrenOf/{existingAlbumId}");
    

        response.EnsureSuccessStatusCode();
    
        var content = await response.Content.ReadAsStringAsync();
        var albumsList = JsonSerializer.Deserialize<AlbumsListResponse>(content);
    
        Assert.NotNull(albumsList);
        Assert.NotEmpty(albumsList.Albums);
    }*/
    
    
    
    #endregion

    #region Albums-related helpers

    #region Create
    
    /// <summary>
    /// Create new album
    /// </summary>
    /// <param name="name">Album name</param>
    /// <param name="parentId">Album parent ID, may be null</param>
    /// <param name="expectedStatusCode">Fail if returned code is not this one</param>
    /// <param name="exitAfterResponseCodeCheck">If true then stop after response code check</param>
    /// <returns>NewAlbumResponse</returns>
    private async Task<NewAlbumResponse> CreateAlbumAsync
    (
        string name,
        Guid? parentId,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK,
        bool exitAfterResponseCodeCheck = false
    )
    {
        var dto = new NewAlbumDto
        {
            Name = name,
            ParentId = parentId
        };

        var request = new NewAlbumRequest
        {
            AlbumToAdd = dto
        };

        // Act
        var response = await _factory.HttpClient.PostAsJsonAsync("/api/Albums/New", request);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);

        if (exitAfterResponseCodeCheck)
        {
            return null;
        }

        var responseData = JsonSerializer.Deserialize<NewAlbumResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); // Did we get response?
        Assert.NotNull(responseData.NewAlbum); // Did we get DTO?

        return responseData;
    }
    
    /// <summary>
    /// A repository of options for what might come
    /// </summary>
    public static IEnumerable<object[]> CreateAlbumData()
    {
        return new List<object[]>
        {
            new object[] { "", false },
            new object[] { "      ", false },
            new object[] { new string('a', 10000) , false },
            new object[] { new string('0', 10000), false },
            new object[] { "4f8c2c6f-1302-408b-b887-19ac1e982736", true },
        };
    }
    
    #endregion

    #region Get top level list

    private List<Album> GetTestAlbums()
    {
        return new List<Album>
        {
            new Album(Guid.NewGuid(), null, "Album1", DateTime.UtcNow),
            new Album(Guid.NewGuid(), null, "Album2", DateTime.UtcNow)
        };
    }
    
    private AlbumsController CreateController()
    {
        return new AlbumsController(_mockAlbumsService.Object);
    }
    
    private void SetupMockAlbumService(List<Album> albums)
    {
        _mockAlbumsService
            .Setup(service => service.GetChildrenAsync(null))
            .ReturnsAsync(albums);
    }

    #endregion

    #region Another

    private HttpClient CreateClient()
    {
        return _factory.CreateClient();
    }

    #endregion
    
    #endregion
}