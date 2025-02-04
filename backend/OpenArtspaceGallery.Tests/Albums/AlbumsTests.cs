using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using OpenArtspaceGallery.Controllers;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Tests.Albums;



public class AlbumsTests : IClassFixture<TestsFactory<Program>>
{
    #region Initialization
    private readonly TestsFactory<Program> _factory;

    public AlbumsTests(TestsFactory<Program> factory)
    {
        _factory = factory;
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
    [InlineData("", false)] // Empty string
    [InlineData("      ", false)] // Some whitespace
    [InlineData(@"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eget eros vitae ante maximus euismod. Aliquam maximus sodales sem et pulvinar. Suspendisse nec mi eu sem finibus tempus. Donec lacinia quam ut est auctor efficitur. Curabitur vitae arcu sapien. Fusce vestibulum lobortis mollis. Ut at tincidunt eros, quis accumsan eros. Ut egestas odio sed commodo dictum. Nullam sed vehicula quam. Aliquam sit amet ex in neque rhoncus vestibulum. Praesent dolor turpis, tempus et massa sed, hendrerit lobortis nunc. Donec dolor justo, laoreet eget dapibus ut, porttitor vehicula nulla.
Quisque tristique lectus id felis imperdiet, a tempor nisl efficitur. Morbi lectus massa, ullamcorper ac nunc non, tincidunt consectetur nisi. Vivamus molestie enim sed enim tempus, at finibus est lobortis. Cras hendrerit enim sed condimentum dapibus. Vivamus finibus pretium libero, id maximus felis congue et. Curabitur sed nunc libero. Cras vitae euismod urna, ac viverra turpis. Donec euismod faucibus sem eu ullamcorper. Mauris blandit enim ac eros cursus, eu malesuada augue tristique. In lobortis vehicula lacus sed fermentum. Cras egestas commodo magna, quis condimentum quam scelerisque nec. Nam blandit enim et magna hendrerit, in ullamcorper arcu ultrices. Curabitur iaculis lacus diam, at tincidunt ex laoreet non. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus ornare et enim egestas malesuada.
Donec consectetur neque nec erat imperdiet sagittis. Donec nec porttitor lectus. Suspendisse auctor euismod egestas. In quis risus massa. Nam sollicitudin finibus leo sit amet mollis. Suspendisse eu ante in nisi laoreet ultricies. Sed a libero leo. Donec dapibus eros non ligula ultricies rhoncus. Morbi feugiat rutrum metus, eget rhoncus quam viverra vitae. Vestibulum vulputate porta nulla sit amet accumsan. Integer finibus augue et congue egestas. Donec ac fermentum augue, a fringilla elit.
Duis eget erat porta, semper purus quis, vehicula metus. Ut elit tortor, egestas blandit nibh vitae, malesuada elementum diam. Cras aliquam ex velit, nec vehicula mauris pharetra non. Integer egestas nunc nisl, sed congue justo euismod sed. Pellentesque lacinia suscipit nisl, ac convallis velit accumsan ac. Etiam tincidunt urna augue, eget dignissim turpis aliquam vel. Maecenas sed urna fringilla, imperdiet augue sed, lacinia nisi. Suspendisse potenti. Fusce vel lorem vitae nibh elementum dapibus in et mi.
Pellentesque porttitor dictum leo, ac interdum risus ullamcorper vitae. Mauris mauris nibh, feugiat eget dolor et, placerat venenatis purus. Vestibulum elementum erat in dapibus dignissim. Pellentesque cursus id tellus non euismod. Vestibulum sapien nisl, elementum eget turpis sed, suscipit sagittis felis. Aliquam faucibus mollis fringilla. Maecenas id dignissim leo. Maecenas venenatis magna eu malesuada fringilla. Duis vehicula, nunc at sollicitudin dapibus, arcu sapien vehicula quam, eu sodales neque orci at nisi. Mauris aliquet ut ligula non lacinia. Etiam sit amet ex posuere, pretium purus ut, scelerisque diam. Curabitur in vulputate sapien, sed cursus nisi. Donec eu est nisi. Nunc eget tempor ante. Cras ac enim elit. "
        , false)] // Too long name
    [InlineData("4f8c2c6f-1302-408b-b887-19ac1e982736", true)] // Correct
    public async Task AddNewAlbum_CheckNameCorrectness(string name, bool isMustBeSuccessful)
    {
        await CreateAlbumAsync(name, null, isMustBeSuccessful ? HttpStatusCode.OK : HttpStatusCode.BadRequest, true);
    }
    

    #endregion
    
    #region Get top level albums

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsOkResultWithCorrectStatusCode()
    {
        var mockAlbumsService = new Mock<IAlbumsService>();

        var albums = new List<Album>
        {
            new Album(Guid.NewGuid(), null, "Album1", DateTime.UtcNow),
            new Album(Guid.NewGuid(), null, "Album2", DateTime.UtcNow)
        };
        
        mockAlbumsService
            .Setup(service => service.GetChildrenAsync(null))
            .ReturnsAsync(albums);

        var controller = new AlbumsController(mockAlbumsService.Object);
        
        var result = await controller.GetTopLevelAlbumsListAsync();
        
        var actionResult = Assert.IsType<ActionResult<AlbumsListResponse>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsEmptyList()
    {
        var mockAlbumsService = new Mock<IAlbumsService>();
        var albums = new List<Album>(); 

        mockAlbumsService
            .Setup(service => service.GetChildrenAsync(null))
            .ReturnsAsync(albums);

        var controller = new AlbumsController(mockAlbumsService.Object);
        
        var result = await controller.GetTopLevelAlbumsListAsync();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<AlbumsListResponse>(okResult.Value);
        Assert.Empty(response.Albums); 
    }
    
    

    #endregion

    #region Albums-related helpers

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

    #endregion
}