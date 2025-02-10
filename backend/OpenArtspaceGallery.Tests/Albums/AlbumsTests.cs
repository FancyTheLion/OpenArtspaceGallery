using System.Net;
using System.Net.Http.Json;
using System.Text;
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
    
    public AlbumsTests(TestsFactory<Program> factory)
    {
        _factory = factory;
    }

#endregion

    #region Create an album

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
    
    [Theory]
    [MemberData(nameof(CreateAlbumData))]
    public async Task AddNewAlbum_CheckNameCorrectness(string name, bool isMustBeSuccessful)
    {
        await CreateAlbumAsync(name, null, isMustBeSuccessful ? HttpStatusCode.OK : HttpStatusCode.BadRequest, true);
    }
    
    #endregion
    
    #region Get top level albums
    
    [Fact]
    public async Task GetTopLevelAlbumsListAsync_ReturnsAlbums()
    {
        // Arrange: creating some top-level albums
        var topLevelAlbumNames = new string[]
        {
            $"Top Level Album { Guid.NewGuid() }",
            $"Top Level Album { Guid.NewGuid() }",
            $"Top Level Album { Guid.NewGuid() }"
        };
        
        foreach (var topLevelAlbumName in topLevelAlbumNames)
        {
            await CreateAlbumAsync(topLevelAlbumName, null);
        }
        
        // Act: get top level albums
        var response = await _factory.HttpClient.GetAsync("/api/albums/TopLevel");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var topLevelAlbumsResult = JsonConvert.DeserializeObject<AlbumsListResponse>(await response.Content.ReadAsStringAsync());

        // Assert: check created albums
        foreach (var topLevelAlbumName in topLevelAlbumNames)
        {
            var topLevelAlbum = topLevelAlbumsResult
                .Albums
                .Single(a => a.Name == topLevelAlbumName);
        
            Assert.Null(topLevelAlbum.Parent);
        }
    }
    
    #endregion
    
    #region Get children albums list
    
    [Fact]
    public async Task GetChildrenAlbumsListAsync_ReturnsChildren_WhenAlbumExists()
    {
        //Arrange
        
        // Parent album
        var parentAlbumName = $"Top level album with child { Guid.NewGuid() }";
        var parentId = (await CreateAlbumAsync(parentAlbumName, null))
            .NewAlbum
            .Id;
        
        // Level 1 child album
        var level1ChildAlbumName = $"Level 1 child album { Guid.NewGuid() }";
        var level1ChildAlbumId = (await CreateAlbumAsync(level1ChildAlbumName, parentId))
            .NewAlbum
            .Id;
        
        // Level 2 child album
        var level2ChildAlbumName = $"Level 2 child album { Guid.NewGuid() }";
        var level2ChildAlbumId = (await CreateAlbumAsync(level2ChildAlbumName, level1ChildAlbumId))
            .NewAlbum
            .Id;
        
        // Act and assert: get parent's children
        
        // Level 1
        var response = await _factory.HttpClient.GetAsync($"/api/Albums/ChildrenOf/{ parentId }");
        response.EnsureSuccessStatusCode();
        
        var level1AlbumsList = JsonSerializer.Deserialize<AlbumsListResponse>(await response.Content.ReadAsStringAsync());
        var level1ChildAlbum = level1AlbumsList
            .Albums
            .Single(a => a.Id == level1ChildAlbumId);
        
        Assert.Equal(parentId, level1ChildAlbum.Parent);
        Assert.Equal(level1ChildAlbumName, level1ChildAlbum.Name);
        
        // Level 2
        response = await _factory.HttpClient.GetAsync($"/api/Albums/ChildrenOf/{ level1ChildAlbumId }");
        response.EnsureSuccessStatusCode();
        
        var level2AlbumsList = JsonSerializer.Deserialize<AlbumsListResponse>(await response.Content.ReadAsStringAsync());
        var level2ChildAlbum = level2AlbumsList
            .Albums
            .Single(a => a.Id == level2ChildAlbumId);
        
        Assert.Equal(level1ChildAlbumId, level2ChildAlbum.Parent);
        Assert.Equal(level2ChildAlbumName, level2ChildAlbum.Name);
    }
    
    #endregion

    #region Get list albums in hierarchy

    /*[Fact]
    public async Task GetListAlbumsInHierarchy_ReturnsAllAlbumsInHierarchy()
    {
        var parentAlbumName = $"Parent Album {Guid.NewGuid()}";
        var parentResponse = await CreateAlbumAsync(parentAlbumName, null);
        Assert.NotNull(parentResponse?.NewAlbum);
        var parentId = parentResponse.NewAlbum.Id;
        
        var level1ChildAlbumName = $"Level 1 Album {Guid.NewGuid()}";
        var level1Response = await CreateAlbumAsync(level1ChildAlbumName, parentId);
        Assert.NotNull(level1Response?.NewAlbum);
        var level1ChildAlbumId = level1Response.NewAlbum.Id;
        
        var level2ChildAlbumName = $"Level 2 Album {Guid.NewGuid()}";
        var level2Response = await CreateAlbumAsync(level2ChildAlbumName, level1ChildAlbumId);
        Assert.NotNull(level2Response?.NewAlbum);
        var level2ChildAlbumId = level2Response.NewAlbum.Id;
        
        // Act
        var response = await _factory.HttpClient.GetAsync($"/api/Albums/Hierarchy/{parentId}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(responseContent), "Response content is empty");
        
        var hierarchyResponse = JsonSerializer.Deserialize<AlbumHierarchyResponse>(responseContent);
        Assert.NotNull(hierarchyResponse);
        Assert.NotNull(hierarchyResponse.AlbumHierarchy);

        // Assert
        var parentAlbum = hierarchyResponse.AlbumHierarchy.SingleOrDefault(a => a.Id == parentId);
        Assert.NotNull(parentAlbum);
        Assert.Equal(parentAlbumName, parentAlbum.Name);

        var level1Album = hierarchyResponse.AlbumHierarchy.SingleOrDefault(a => a.Id == level1ChildAlbumId);
        Assert.NotNull(level1Album);
        Assert.Equal(level1ChildAlbumName, level1Album.Name);

        var level2Album = hierarchyResponse.AlbumHierarchy.SingleOrDefault(a => a.Id == level2ChildAlbumId);
        Assert.NotNull(level2Album);
        Assert.Equal(level2ChildAlbumName, level2Album.Name);
    }*/

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteAlbumAsync_RemovesAlbumSuccessfully()
    {
        var albumName = $"Test Album {Guid.NewGuid()}";
        var createResponse = await CreateAlbumAsync(albumName, null);
    
        Assert.NotNull(createResponse?.NewAlbum);
        var albumId = createResponse.NewAlbum.Id;
        
        var deleteResponse = await _factory.HttpClient.DeleteAsync($"/api/Albums/{albumId}");
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        
        var deleteAgainResponse = await _factory.HttpClient.DeleteAsync($"/api/Albums/{albumId}");
        Assert.Equal(HttpStatusCode.NotFound, deleteAgainResponse.StatusCode);
    }

    #endregion

    #region Rename

    [Fact]
    public async Task RenameAlbum_ReturnsOk_WhenValidData()
    {
        var albumName = "Old Album Name";
        var newAlbumResponse = await CreateAlbumAsync(albumName, parentId: null);
        var albumId = newAlbumResponse.NewAlbum.Id;
        
        var renameRequest = new RenameAlbumRequest
        {
            RenameAlbumInfo = new RenameAlbumDto { NewName = "New Album Name" }
        };

        var content = new StringContent(JsonSerializer.Serialize(renameRequest), Encoding.UTF8, "application/json");

        // Act
        var response = await _factory.HttpClient.PostAsync($"/api/albums/{albumId}/Rename", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


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
    
    #endregion
    
    #endregion
}