using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;
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
        
        var response = await AddAsync(name, width, height);
    
        Assert.Equal(name, response.ImageSize.Name);
        Assert.Equal(width, response.ImageSize.Width);
        Assert.Equal(height, response.ImageSize.Height); // TODO: Add a method to the controller and here GetInfo
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

    #region Get list

    [Fact]
    public async Task GetListAsync_WithValidData_ReturnsImagesSizes()
    {
        var imageSize1 = new { Name = $"Image size 1 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var imageSize2 = new { Name = $"Image size 2 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        var imageSize3 = new { Name = $"Image size 3 {Guid.NewGuid()}", Width = Random.Shared.Next(10, 16999), Height = Random.Shared.Next(10, 16999) };
        
        // Is it possible to do this, "bare" requests
        await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        await AddAsync(imageSize2.Name, imageSize2.Width, imageSize2.Height);
        await AddAsync(imageSize3.Name, imageSize3.Width, imageSize3.Height);

        var response = (await GetListAsync());
        
        Assert.Contains(response.ImagesSizes, x => x.Name == imageSize1.Name && x.Width == imageSize1.Width && x.Height == imageSize1.Height);
        Assert.Contains(response.ImagesSizes, x => x.Name == imageSize2.Name && x.Width == imageSize2.Width && x.Height == imageSize2.Height);
        Assert.Contains(response.ImagesSizes, x => x.Name == imageSize3.Name && x.Width == imageSize3.Width && x.Height == imageSize3.Height);
    }
    
    [Fact]
    public async Task GetListAsync_WithNoData_ReturnsEmptyList()
    {
        Assert.Empty((await GetListAsync()).ImagesSizes);
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

    #region Get list
    
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

    #endregion
}