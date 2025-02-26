using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.Shared;
using Xunit.Abstractions;

namespace OpenArtspaceGallery.Tests.ImagesSizes;

public class ImagesSizesTests : IClassFixture<TestsFactory<Program>>
{
    #region Initialization
    
    private readonly TestsFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    
    public ImagesSizesTests(TestsFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    #endregion

    #region Create image size

    [Fact]
    public async Task AddImageSize_WithValidData_ReturnsCreatedImageSize()
    {
        var name = $"Test size {Guid.NewGuid()}";
        var width = Random.Shared.Next(10, 16999); // TODO: Constants in appsettings.json
        var height = Random.Shared.Next(10, 16999); // TODO: Constants in appsettings.json
        
        var addResponse = await AddAsync(name, width, height);
    
        Assert.Equal(name, addResponse.ImageSize.Name);
        Assert.Equal(width, addResponse.ImageSize.Width);
        Assert.Equal(height, addResponse.ImageSize.Height);

        var getResponse = await GetInfoAsync(addResponse.ImageSize.Id);
        
        Assert.Equal(name, getResponse.ImageSize.Name);
        Assert.Equal(width, getResponse.ImageSize.Width);
        Assert.Equal(height, getResponse.ImageSize.Height);
    }
    
    public static IEnumerable<object[]> AddTestDimensionsData()
    {
        return new List<object[]>
        {
            new object[] { $"InvalidSize1 { Guid.NewGuid() }", 0, $"{ Random.Shared.Next(10, 16999) }" },
            new object[] { $"InvalidSize2 { Guid.NewGuid() }", $"{ Random.Shared.Next(10, 16999) }", 0 },
            new object[] { $"InvalidSize3 { Guid.NewGuid() }", $"{ Random.Shared.Next(-100, -1) }", 200 },
            new object[] { $"InvalidSize4 { Guid.NewGuid() }", 200, $"{ Random.Shared.Next(-200, -101) }" }
        };
    }

    [Theory]
    [MemberData(nameof(AddTestDimensionsData))]
    public async Task AddImageSize_WithInvalidDimensions_ReturnsBadRequest(string name, int width, int height)
    {
        await AddAsync(name, width, height, HttpStatusCode.BadRequest, exitAfterResponseCodeCheck: true);
    }
    
    #endregion

    #region Get info

    [Fact]
    public async Task GetInfoAsync_WithValidData_ReturnsImageSize()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var addResponse = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);

        var response = await GetInfoAsync(addResponse.ImageSize.Id);
        
        Assert.Equal(imageSize1.Name, response.ImageSize.Name);
        Assert.Equal(imageSize1.Width, response.ImageSize.Width);
        Assert.Equal(imageSize1.Height, response.ImageSize.Height);
    }

    #endregion

    #region Get list

    [Fact]
    public async Task GetListAsync_WithValidData_ReturnsImagesSizes()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var imageSize2 = new { Name = $"Image size 2 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var imageSize3 = new { Name = $"Image size 3 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        
        // Is it possible to do this, "bare" requests?
        await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        await AddAsync(imageSize2.Name, imageSize2.Width, imageSize2.Height);
        await AddAsync(imageSize3.Name, imageSize3.Width, imageSize3.Height);

        var response = (await GetListAsync());
        
        Assert.Contains(response.ImagesSizes, x => x.Name == imageSize1.Name && x.Width == imageSize1.Width && x.Height == imageSize1.Height);
        Assert.Contains(response.ImagesSizes, x => x.Name == imageSize2.Name && x.Width == imageSize2.Width && x.Height == imageSize2.Height);
        Assert.Contains(response.ImagesSizes, x => x.Name == imageSize3.Name && x.Width == imageSize3.Width && x.Height == imageSize3.Height);
    }
    
    #endregion

    #region Delete
    
    [Fact]
    public async Task DeleteAsync_WithValidData()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var response = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        
        Assert.True(await IsExistsAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height));

        await DeleteAsync(response.ImageSize.Id);
        
        Assert.False(await IsExistsAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height));
    }

    #endregion

    #region Update

    [Fact]
    public async Task UpdateAsync_WithValidData()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var addResponse = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        Assert.True(await IsExistsAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height));
        
        var imageSize2 = new { Name = $"Image size 2 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        
        var imageSize2Dto = new ImageSizeDto (addResponse.ImageSize.Id, imageSize2.Name, imageSize2.Width,  imageSize2.Height);

        var request = new UpdateImageSizeByIdRequest { ImageSize = imageSize2Dto };
        
        var updateResponse = await _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/UpdateById", request);
        updateResponse.EnsureSuccessStatusCode();
        
        Assert.True(await IsExistsAsync(imageSize2.Name, imageSize2.Width, imageSize2.Height));
    }
    
    [Fact]
    public async Task UpdateAsync_WithNotExistenceData_ReturnNotFound()
    {
        var imageSize2Dto = new ImageSizeDto (Guid.NewGuid(), $"Image size 1 {Guid.NewGuid()}", Random.Shared.Next(10, 16999), Random.Shared.Next(10, 16999));
        var request = new UpdateImageSizeByIdRequest { ImageSize = imageSize2Dto };
        
        var updateResponse = await _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/UpdateById", request);
        
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
    }
    
    [Fact]
    public async Task UpdateAsync_WithNullValue_ReturnBadRequest()
    {
        var request = new StringContent("null", Encoding.UTF8, "application/json");

        var updateResponse = await _factory.HttpClient.PostAsync("api/ImagesSizes/UpdateById", request);
        
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
    }

    #endregion

    #region Existence

    [Fact]
    public async Task IsExistAsync_ReturnsTrue_WhenImageSizeExists()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        
        Assert.True(await IsExistsAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height));
    }
    
    [Fact]
    public async Task IsExistAsync_ReturnsFalse_DoesNotExist()
    {
        Assert.False(await IsExistsAsync($"Image size 1 {Guid.NewGuid()}",Random.Shared.Next(10, 16999),Random.Shared.Next(10, 16999)));
    }

    #endregion
    
    #region Existence by name 
    
    [Fact]
    public async Task IsExistByNameAsync()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var response = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        
        Assert.True(await IsExistsByNameAsync(imageSize1.Name));
        
        await DeleteAsync(response.ImageSize.Id);
        
        Assert.False(await IsExistsByNameAsync(imageSize1.Name));
    }

    #endregion

    #region Existence by dimensions

    [Fact]
    public async Task IsExistByDimensionsAsync()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var response = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        
        Assert.True(await IsExistsByDimensionsAsync(imageSize1.Width, imageSize1.Height));
        
        await DeleteAsync(response.ImageSize.Id);
        
        Assert.False(await IsExistsByDimensionsAsync(imageSize1.Width, imageSize1.Height));
    }

    #endregion
    
    #region Helpers

    #region Create

    /// <summary>
    /// Create new image size
    /// </summary>
    /// <param name="name">Name image size</param>
    /// <param name="width">Width image size </param>
    /// <param name="height">Height image size</param>
    /// <param name="expectedStatusCode">Fail if returned code is not this one</param>
    /// <param name="exitAfterResponseCodeCheck">If true then stop after response code check</param>
    /// <returns>AddImageSizeResponse</returns>
    private async Task<AddImageSizeResponse> AddAsync(string name, int width, int height, HttpStatusCode expectedStatusCode = HttpStatusCode.OK, bool exitAfterResponseCodeCheck = false)
    {
        var dto = new AddImageSizeDto
        {
            Name = name,
            Width = width,
            Height = height,
        };

        var request = new AddImageSizeRequest
        {
            ImageSize = dto
        };
        
        var response = await _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/Add", request);
        
        Assert.Equal(expectedStatusCode, response.StatusCode);
        
        if (exitAfterResponseCodeCheck)
        {
            return null;
        }
        
        var responseData = JsonSerializer.Deserialize<AddImageSizeResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); // Did we get response?
        Assert.NotNull(responseData.ImageSize); // Did we get DTO?

        return responseData;
    }


    #endregion

    #region Get

    /// <summary>
    /// Get image size by ID
    /// </summary>
    /// <param name="id">Image size ID</param>
    /// <param name="exitAfterResponseCodeCheck">Exit immediately after response code check</param>
    /// <param name="expectedStatusCode">Response must have this code</param>
    /// <returns>Image size</returns>
    private async Task<ImageSizeResponse?> GetInfoAsync
    (
        Guid id,
        bool exitAfterResponseCodeCheck = false,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var response = await _factory.HttpClient.GetAsync($"api/ImagesSizes/{id}");
        
        Assert.Equal(expectedStatusCode, response.StatusCode);
        
        if (exitAfterResponseCodeCheck)
        {
            return null;
        }
        
        var responseData = JsonSerializer.Deserialize<ImageSizeResponse?>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); // Did we get response?
        Assert.NotNull(responseData.ImageSize); // Did we get DTO?

        return responseData;
    }
    
    
    /// <summary>
    /// Get a list of image sizes
    /// </summary>
    private async Task<ImagesSizesResponse> GetListAsync()
    {
        var response = _factory.HttpClient.GetAsync("api/ImagesSizes/GetList");
        response.Result.EnsureSuccessStatusCode();
        
        var listResponse = JsonSerializer.Deserialize<ImagesSizesResponse>(await response.Result.Content.ReadAsStringAsync());
        
        Assert.NotNull(listResponse);
        Assert.NotNull(listResponse.ImagesSizes);

        return listResponse;
    }

    #endregion

    #region Delete

    private async Task DeleteAsync(Guid id)
    {
        var deleteResponse = _factory.HttpClient.DeleteAsync($"api/ImagesSizes/{id}");
        deleteResponse.Result.EnsureSuccessStatusCode();
    }

    #endregion

    #region Is exist

    private async Task<bool> IsExistsAsync(string name, int width, int height)
    {
        var dto = new ImageSizeBaseDto
        (
             name,
             width,
             height
        );
        
        var request = new ImageSizeExistenceRequest
        {
            ImageSize = dto
        };
        
        var isExistResponse = _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/IsExists", request);
        
        isExistResponse.Result.EnsureSuccessStatusCode();
        
        return JsonSerializer.Deserialize<ExistenceResponse>(await isExistResponse.Result.Content.ReadAsStringAsync())
            .Existence
            .Exists;
    }
    
    private async Task<bool> IsExistsByNameAsync(string name)
    {
        var dto = new ImageSizeExistenceByNameDto
        {
            Name = name
        };
        
        var request = new ImageSizeNameExistenceRequest
        {
            ImageSizeExistenceByName = dto
        };
        
        var isExistResponse = _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/IsExistByName", request);
        
        isExistResponse.Result.EnsureSuccessStatusCode();

        return JsonSerializer
            .Deserialize<ImageSizeNameExistenceResponse>(await isExistResponse.Result.Content.ReadAsStringAsync())
            .NameExistence
            .Exists;
    }
    
    private async Task<bool> IsExistsByDimensionsAsync(int width, int height)
    {
        var dto = new ImageSizeDimensionsDto
        {
            Width = width,
            Height = height
        };
        
        var request = new ImageSizeDimensionsExistenceRequest
        {
            ImageSizeDimensionsExistence = dto
        };
        
        var isExistResponse = _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/IsExistByDimensions", request);
        
        isExistResponse.Result.EnsureSuccessStatusCode();

        return JsonSerializer
            .Deserialize<ImageSizeDimensionsExistenceResponse>(await isExistResponse.Result.Content.ReadAsStringAsync())
            .DimensionsExistence
            .Exists;
    }
    
    #endregion

    #endregion
}