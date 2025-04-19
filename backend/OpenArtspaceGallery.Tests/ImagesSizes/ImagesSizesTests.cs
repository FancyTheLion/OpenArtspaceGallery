using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.Shared;
using OpenArtspaceGallery.Models.Settings;
using OpenArtspaceGallery.Tests.Helpers;
using Xunit.Abstractions;

namespace OpenArtspaceGallery.Tests.ImagesSizes;

public class ImagesSizesTests : IClassFixture<TestsFactory<Program>>
{
    private readonly TestsFactory<Program> _factory;
    
    #region Initialization
    
    public ImagesSizesTests
    (
        TestsFactory<Program> factory
    )
    {
        _factory = factory;
    }

    #endregion

    #region Create image size

    [Fact]
    public async Task AddImageSize_WithValidData_ReturnsCreatedImageSize()
    {
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
        var addResponse = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
    
        Assert.Equal(imageSize1.Name, addResponse.ImageSize.Name);
        Assert.Equal(imageSize1.Width, addResponse.ImageSize.Width);
        Assert.Equal(imageSize1.Height, addResponse.ImageSize.Height);

        var getResponse = await GetInfoAsync(addResponse.ImageSize.Id);
        
        Assert.Equal(imageSize1.Name, getResponse.ImageSize.Name);
        Assert.Equal(imageSize1.Width, getResponse.ImageSize.Width);
        Assert.Equal(imageSize1.Height, getResponse.ImageSize.Height);
    }

    [Fact]
    public async Task AddImageSize_WithInvalidDimensions_ReturnsBadRequest()
    {
        var settings = _factory.Configuration.GetSection(nameof(ImagesSizesSettings)).Get<ImagesSizesSettings>();
        
        var testCases = new List<Tuple<string, int, int>>
        {
            new ($"InvalidSize1 { Guid.NewGuid() }", settings.MinWidth - 1, settings.MinHeight + 1),
            new ($"InvalidSize2 { Guid.NewGuid() }", settings.MinWidth + 1, settings.MinHeight - 1),
            new ($"InvalidSize3 { Guid.NewGuid() }", settings.MaxWidth + 1, settings.MinHeight + 1),
            new ($"InvalidSize4 { Guid.NewGuid() }", settings.MinWidth + 1, settings.MaxHeight + 1)
        };

        foreach (var testCase in testCases)
        {
            await AddAsync(testCase.Item1, testCase.Item2, testCase.Item3, HttpStatusCode.BadRequest, exitAfterResponseCodeCheck: true);
        }
    }
    
    #endregion

    #region Get info

    [Fact]
    public async Task GetInfoAsync_WithValidData_ReturnsImageSize()
    {
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
        var addResponse = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);

        var response = await GetInfoAsync(addResponse.ImageSize.Id);
        
        Assert.Equal(addResponse.ImageSize.Name, response.ImageSize.Name);
        Assert.Equal(addResponse.ImageSize.Width, response.ImageSize.Width);
        Assert.Equal(addResponse.ImageSize.Height, response.ImageSize.Height);
    }
    
    [Fact]
    public async Task GetInfoAsync_WithInvalidId_ReturnsNotFound()
    {
        await GetInfoAsync(Guid.NewGuid(), true, HttpStatusCode.NotFound);
    }

    #endregion

    #region Get list

    [Fact]
    public async Task GetListAsync_WithValidData_ReturnsImagesSizes()
    {
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
        var imageSize2 = _factory.ImagesSizesHelper.GetNextImageSize();
        var imageSize3 = _factory.ImagesSizesHelper.GetNextImageSize();
        
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
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
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
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
        var addResponse = await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        Assert.True(await IsExistsAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height));
        
        var imageSize2 = _factory.ImagesSizesHelper.GetNextImageSize();

        var updateResponse = await UpdateAsync(addResponse.ImageSize.Id, imageSize2.Name, imageSize2.Width,  imageSize2.Height);
        
        var afterUpdateReadback = await GetInfoAsync(addResponse.ImageSize.Id);
        
        Assert.Equal(imageSize2.Name, afterUpdateReadback.ImageSize.Name);
        Assert.Equal(imageSize2.Width, afterUpdateReadback.ImageSize.Width);
        Assert.Equal(imageSize2.Height, afterUpdateReadback.ImageSize.Height);
    }
    
    [Fact]
    public async Task UpdateAsync_WithNotExistenceData_ReturnNotFound()
    {
        var ImageSize = _factory.ImagesSizesHelper.GetNextImageSize().ToDto();
        
        var updateResponse = await UpdateAsync(ImageSize.Id, ImageSize.Name, ImageSize.Width,  ImageSize.Height, exitAfterResponseCodeCheck: true, HttpStatusCode.NotFound);
    }

    #endregion

    #region Existence

    [Fact]
    public async Task IsExistAsync_ReturnsTrue_WhenImageSizeExists()
    {
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
        await AddAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height);
        
        Assert.True(await IsExistsAsync(imageSize1.Name, imageSize1.Width, imageSize1.Height));
    }
    
    [Fact]
    public async Task IsExistAsync_ReturnsFalse_DoesNotExist()
    {
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
        Assert.False(await IsExistsAsync(imageSize1.Name,imageSize1.Width, imageSize1.Height));
    }

    #endregion
    
    #region Existence by name 
    
    [Fact]
    public async Task IsExistByNameAsync()
    {
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
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
        var imageSize1 = _factory.ImagesSizesHelper.GetNextImageSize();
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

    #region Update

    private async Task<UpdateImageSizeByIdResponse> UpdateAsync
    (
        Guid id,
        string name,
        int width,
        int height,
        bool exitAfterResponseCodeCheck = false,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        var updateRequest = new UpdateImageSizeByIdRequest
        {
            ImageSize = new ImageSizeDto(id, name, width, height, false)
        };
        
        var response = await _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/UpdateById", updateRequest);
        
        Assert.Equal(expectedStatusCode, response.StatusCode);
        
        if (exitAfterResponseCodeCheck)
        {
            return null;
        }
        
        var responseData = JsonSerializer.Deserialize<UpdateImageSizeByIdResponse?>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); // Did we get response?
        Assert.NotNull(responseData.ImageSize); // Did we get DTO?

        return responseData;
    }

    #endregion

    #region Is exist

    private async Task<bool> IsExistsAsync(string name, int width, int height)
    {
        var request = new ImageSizeExistenceRequest
        {
            ImageSize = new ImageSizeBaseDto
            (
                name,
                width,
                height,
                false
            )
        };
        
        var isExistResponse = _factory.HttpClient.PostAsJsonAsync("api/ImagesSizes/IsExists", request);
        
        isExistResponse.Result.EnsureSuccessStatusCode();
        
        return JsonSerializer.Deserialize<ExistenceResponse>(await isExistResponse.Result.Content.ReadAsStringAsync())
            .Existence
            .Exists;
    }
    
    private async Task<bool> IsExistsByNameAsync(string name)
    {
        var request = new ImageSizeNameExistenceRequest
        {
            ImageSizeExistenceByName = new ImageSizeExistenceByNameDto
            {
                Name = name
            }
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
        var request = new ImageSizeDimensionsExistenceRequest
        {
            ImageSizeDimensionsExistence = new ImageSizeDimensionsDto
            {
                Width = width,
                Height = height
            }
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