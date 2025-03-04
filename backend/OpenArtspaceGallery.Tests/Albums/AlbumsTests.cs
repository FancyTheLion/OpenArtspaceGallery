using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OpenArtspaceGallery.Controllers;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Albums;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.Responses.Albums;
using OpenArtspaceGallery.Models.API.Responses.Shared;
using OpenArtspaceGallery.Services.Abstract;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OpenArtspaceGallery.Tests.Albums;

public class AlbumsTests : IClassFixture<TestsFactory<Program>>
{
    #region Initialization

    private readonly TestsFactory<Program> _factory;
    private readonly ITestOutputHelper _output;


    public AlbumsTests(TestsFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    #endregion

    #region Create an album

    public static IEnumerable<object[]> AddNewAlbum_CheckNameTrim_DataGenerator()
    {
        var baseName = $"Megaalbum!{Guid.NewGuid()}";

        return new List<object[]>
        {
            new object[] { baseName, baseName },
            new object[] { $"    {baseName}", baseName },
            new object[] { $"{baseName}    ", baseName },
            new object[] { $"    {baseName}    ", baseName },
            new object[] { $"Prefix {baseName}", $"Prefix {baseName}" }
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
            new object[] { new string('a', 10000), false },
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
            $"Top Level Album {Guid.NewGuid()}",
            $"Top Level Album {Guid.NewGuid()}",
            $"Top Level Album {Guid.NewGuid()}"
        };

        foreach (var topLevelAlbumName in topLevelAlbumNames)
        {
            await CreateAlbumAsync(topLevelAlbumName, null);
        }

        // Act: get top level albums
        var response = await _factory.HttpClient.GetAsync("/api/albums/TopLevel");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var topLevelAlbumsResult =
            JsonConvert.DeserializeObject<AlbumsListResponse>(await response.Content.ReadAsStringAsync());

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
        var parentAlbumName = $"Top level album with child {Guid.NewGuid()}";
        var parentId = (await CreateAlbumAsync(parentAlbumName, null))
            .NewAlbum
            .Id;

        // Level 1 child album
        var level1ChildAlbumName = $"Level 1 child album {Guid.NewGuid()}";
        var level1ChildAlbumId = (await CreateAlbumAsync(level1ChildAlbumName, parentId))
            .NewAlbum
            .Id;

        // Level 2 child album
        var level2ChildAlbumName = $"Level 2 child album {Guid.NewGuid()}";
        var level2ChildAlbumId = (await CreateAlbumAsync(level2ChildAlbumName, level1ChildAlbumId))
            .NewAlbum
            .Id;

        // Act and assert: get parent's children

        // Level 1
        var response = await _factory.HttpClient.GetAsync($"/api/Albums/ChildrenOf/{parentId}");
        response.EnsureSuccessStatusCode();

        var level1AlbumsList =
            JsonSerializer.Deserialize<AlbumsListResponse>(await response.Content.ReadAsStringAsync());
        var level1ChildAlbum = level1AlbumsList
            .Albums
            .Single(a => a.Id == level1ChildAlbumId);

        Assert.Equal(parentId, level1ChildAlbum.Parent);
        Assert.Equal(level1ChildAlbumName, level1ChildAlbum.Name);

        // Level 2
        response = await _factory.HttpClient.GetAsync($"/api/Albums/ChildrenOf/{level1ChildAlbumId}");
        response.EnsureSuccessStatusCode();

        var level2AlbumsList =
            JsonSerializer.Deserialize<AlbumsListResponse>(await response.Content.ReadAsStringAsync());
        var level2ChildAlbum = level2AlbumsList
            .Albums
            .Single(a => a.Id == level2ChildAlbumId);

        Assert.Equal(level1ChildAlbumId, level2ChildAlbum.Parent);
        Assert.Equal(level2ChildAlbumName, level2ChildAlbum.Name);
    }

    #endregion

    #region Get list albums in hierarchy

    [Fact]
    public async Task GetListAlbumsInHierarchy_ReturnsCorrectHierarchy()
    {
        var parentName = $"Parent Album {Guid.NewGuid()}";
        var parentResponse = await CreateAlbumAsync(parentName, null);
        var parentId = parentResponse.NewAlbum.Id;

        var level1Name = $"Level 1 Album {Guid.NewGuid()}";
        var level1Response = await CreateAlbumAsync(level1Name, parentId);
        var level1Id = level1Response.NewAlbum.Id;

        var level2Name = $"Level 2 Album {Guid.NewGuid()}";
        var level2Response = await CreateAlbumAsync(level2Name, level1Id);
        var level2Id = level2Response.NewAlbum.Id;

        var hierarchy = (await GetHierarchyAsync(level2Id)).ToList();
        
        var parentAlbum = hierarchy[0];
        Assert.NotNull(parentAlbum);
        Assert.Equal(parentId, parentAlbum.Id);
        Assert.Equal(parentName, parentAlbum.Name);

        var level1Album = hierarchy[1];
        Assert.NotNull(level1Album);
        Assert.Equal(level1Id, level1Album.Id);
        Assert.Equal(level1Name, level1Album.Name);
        
        var level2Album = hierarchy[2];
        Assert.NotNull(level2Album);
        Assert.Equal(level2Id, level2Album.Id);
        Assert.Equal(level2Name, level2Album.Name);
    }

    [Fact]
    public async Task GetListAlbumsInHierarchy_ReturnsNotFound()
    {
        var response = await _factory.HttpClient.GetAsync($"/api/Albums/Hierarchy/{ Guid.NewGuid() }");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteAlbumAsync_RemovesAlbumSuccessfully()
    {
        var albumId = (await CreateAlbumAsync($"Test Album {Guid.NewGuid()}", null))
            .NewAlbum
            .Id;

        Assert.True(await IsAlbumExistsAsync(albumId));

        await DeleteAlbumAsync(albumId);

        Assert.False(await IsAlbumExistsAsync(albumId));
    }

    #endregion

    #region Rename

    [Fact]
    public async Task RenameAlbum_ReturnsOk_WhenValidData()
    {
        // Arrange
        var albumId = (await CreateAlbumAsync($"Old Album Name {Guid.NewGuid()}", parentId: null))
            .NewAlbum
            .Id;

        var expectedAlbumName = $"New Album {Guid.NewGuid()}";

        // Act
        await RenameAlbumAsync(albumId, expectedAlbumName);

        var actualAlbumName = (await GetAlbumByIdAsync(albumId))?.Name;

        // Assert
        Assert.Equal(expectedAlbumName, actualAlbumName);
    }

    #endregion

    #region Existence

    [Fact]
    public async Task IsAlbumExistsAsync_ReturnsTrue_WhenAlbumExists()
    {
        var albumId = (await CreateAlbumAsync($"Test Album {Guid.NewGuid()}", null))
            .NewAlbum
            .Id;

        Assert.True(await IsAlbumExistsAsync(albumId));
    }

    [Fact]
    public async Task IsAlbumExistsAsync_ReturnsFalse_DoesNotExist()
    {
        Assert.False(await IsAlbumExistsAsync(Guid.NewGuid()));
    }

    #endregion

    #region Get album's information

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GetAlbumInfoAsync_ReturnInfo_WhenAlbumExists(bool isParentNotNull)
    {
        // Arrange
        Guid? parentId = null;
        if (isParentNotNull == true)
        {
            parentId = (await CreateAlbumAsync($"Test Parent Album { Guid.NewGuid() }", null))
                .NewAlbum
                .Id;            
        }

        var albumName = $"Test Album { Guid.NewGuid() }";
        var albumId = (await CreateAlbumAsync(albumName, parentId))
            .NewAlbum
            .Id;

        // Act
        var albumInfo = (await GetAlbumByIdAsync(albumId));

        // Assert
        Assert.Equal(albumId, albumInfo?.Id);
        Assert.Equal(albumName, albumInfo?.Name);
        Assert.Equal(parentId, albumInfo?.Parent);
    }

    [Fact]
    public async Task GetAlbumInfoAsync_ReturnNotFound_DoesNotExist()
    {
        var response = await _factory.HttpClient.GetAsync($"/api/albums/{ Guid.NewGuid() }");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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

    #region Delete

    /// <summary>
    /// Delete album
    /// </summary>
    /// <param name="albumId">Album ID</param>
    private async Task DeleteAlbumAsync(Guid albumId)
    {
        var deleteResponse = await _factory.HttpClient.DeleteAsync($"/api/Albums/{albumId}");
        deleteResponse.EnsureSuccessStatusCode();
    }

    #endregion

    #region Rename

    /// <summary>
    /// Rename given album
    /// </summary>
    /// <param name="albumId">Album ID</param>
    /// <param name="newName">New name</param>
    private async Task RenameAlbumAsync(Guid albumId, string newName)
    {
        var renameRequest = new RenameAlbumRequest
        {
            RenameAlbumInfo = new RenameAlbumDto { NewName = newName }
        };

        var response = await _factory.HttpClient.PostAsJsonAsync($"/api/albums/{albumId}/Rename", renameRequest);

        response.EnsureSuccessStatusCode();
    }

    #endregion

    #region Get album by id

    /// <summary>
    /// Get album info
    /// </summary>
    /// <param name="albumId">Album ID</param>
    /// <returns></returns>
    private async Task<AlbumDto?> GetAlbumByIdAsync(Guid albumId)
    {
        var response = await _factory.HttpClient.GetAsync($"/api/albums/{albumId}");
        response.EnsureSuccessStatusCode();

        return (JsonSerializer.Deserialize<AlbumInfoResponse>(await response.Content.ReadAsStringAsync()))
            .Album;
    }

    #endregion

    #region Existence

    /// <summary>
    /// Check album existence
    /// </summary>
    /// <param name="albumId">Album ID</param>
    /// <returns>True if album exists</returns>
    private async Task<bool> IsAlbumExistsAsync(Guid albumId)
    {
        var response = await _factory.HttpClient.GetAsync($"/api/albums/{albumId}/IsExists");
        response.EnsureSuccessStatusCode();

        return (JsonSerializer.Deserialize<ExistenceResponse>(await response.Content.ReadAsStringAsync()))
            .Existence
            .Exists;
    }

    #endregion

    #region Get hierarchy

    /// <summary>
    /// Get album's hierarchy from root album down to album with given ID
    /// </summary>
    private async ValueTask<IReadOnlyCollection<AlbumInHierarchyDto>> GetHierarchyAsync(Guid id)
    {
        var response = await _factory.HttpClient.GetAsync($"/api/Albums/Hierarchy/{id}");
        response.EnsureSuccessStatusCode();

        var hierarchyResponse = JsonSerializer.Deserialize<AlbumHierarchyResponse>(await response.Content.ReadAsStringAsync());
        Assert.NotNull(hierarchyResponse);
        Assert.NotNull(hierarchyResponse.AlbumHierarchy);
        Assert.NotEmpty(hierarchyResponse.AlbumHierarchy);

        return hierarchyResponse.AlbumHierarchy;
    }

#endregion

    #endregion
}