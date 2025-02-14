using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

namespace OpenArtspaceGallery.Tests.ImagesSizes;

public class ImagesSizesTests : IClassFixture<TestsFactory<Program>>
{
    #region Initialization
    
    private readonly TestsFactory<Program> _factory;
    
    public ImagesSizesTests(TestsFactory<Program> factory)
    {
        _factory = factory;
    }

    #endregion

    #region Create image size

    

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

    #endregion
}