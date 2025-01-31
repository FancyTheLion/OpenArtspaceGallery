using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;

namespace OpenArtspaceGallery.Tests.SiteInfo;

public class SiteInfoTests  : IClassFixture<TestsFactory<Program>>
{
    #region Initialization

    private readonly HttpClient _client;
    private readonly string _expectedVersion;
    private readonly string _expectedSourcesLink;
    
    public SiteInfoTests(TestsFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Testing.json", optional: false, reloadOnChange: true)
            .Build();
        
        _expectedVersion = configuration["SiteInfoSettings:Version"] ?? throw new InvalidOperationException("BackendVersion not found in appsettings.Testing.json");
        _expectedSourcesLink = configuration["SiteInfoSettings:SourcesUrl"] ?? throw new InvalidOperationException("SourcesLink not found in appsettings.Testing.json");
    }

    #endregion

    #region Backend version

    /// <summary>
    /// Testing the received version with the expected one
    /// </summary>
    [Fact]
    public async Task GetBackendVersion_ShouldReturnCorrectVersion()
    {
        // Arrange
        var expectedVersion = _expectedVersion;

        var responseData = await GetBackendVersionAsync();
        Assert.Equal(expectedVersion, responseData.BackendVersion.Version);
        
        //I wanted to make both tests check different things, without a signature version 
        //"I check the correctness of the incoming version + different variations of the entered version".
        //But in its current form it turns out that I need to remove the second method GetBackendVersion_TestingProcessingVariousInputData(Same thing with the link)
    }

    /// <summary>
    /// Testing different incoming data
    /// </summary>
    [Theory]
    [MemberData(nameof(GetVersionsData))]
    public async Task GetBackendVersion_TestingProcessingVariousInputData(string version)
    {
        var expectedVersion = _expectedVersion;
        var responseData = await GetBackendVersionAsync();
        Assert.Equal(expectedVersion, responseData.BackendVersion.Version);
    }

    [Fact]
    public async Task GetBackendVersion_CheckTypeVersion()
    {
        var responseData = await GetBackendVersionAsync();
        Assert.IsType<string>(responseData.BackendVersion.Version);
    }
    
    /// <summary>
    /// Checking the test version format
    /// </summary>
    [Fact]
    public async Task GetBackendVersion_CheckVersionExpectedFormat()
    {
        var versionPattern = @"^\d+\.\d+\.\d+$";
        var responseData = await GetBackendVersionAsync();
        Assert.Matches(versionPattern, responseData.BackendVersion.Version);
    }
    
    /// <summary>
    /// Checking the test format for json
    /// </summary>
    [Fact]
    public async Task GetBackendVersion_CheckTypeResponse()
    {
        var response = await _client.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        response.EnsureSuccessStatusCode();
        
        Assert.NotNull(response);
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
    }

    #endregion
    
    #region Sources link

    /// <summary>
    /// Checking the received sources link against the expected one
    /// </summary>
    [Fact]
    public async Task GetSourcesLink_ShouldReturnCorrectLink()
    {
        var expectedLink = _expectedSourcesLink;
        
        var responseData = await GetSourcesLinkAsync();
        Assert.Equal(expectedLink, responseData.SourcesLink.SourcesLink);
        
        //I wanted to make both tests check different things, without a signature version 
        //"I check the correctness of the incoming version + different variations of the entered version".
        //But in its current form it turns out that I need to remove the second method GetBackendVersion_TestingProcessingVariousInputData(Same thing with the link)
    }
    
    /// <summary>
    /// Testing different incoming data
    /// </summary>
    [Theory]
    [MemberData(nameof(GetSourcesLinkData))]
    public async Task GetSourcesLink_TestingProcessingVariousInputData(string version)
    {
        var expectedLink = _expectedSourcesLink;

        var responseData = await GetSourcesLinkAsync();
        Assert.Equal(expectedLink, responseData.SourcesLink.SourcesLink);
    }
    
    [Fact]
    public async Task GetSourcesLink_CheckTypeSourcesLink()
    {
        var responseData = await GetSourcesLinkAsync();
        Assert.IsType<string>(responseData.SourcesLink.SourcesLink);
    }
    
    /// <summary>
    /// Checking the test url format
    /// </summary>
    [Fact]
    public async Task GetSourcesLink_CheckUrlExpectedFormat()
    {
        var gitUrlPattern = @"^https:\/\/github\.com\/[a-zA-Z0-9\-]+\/[a-zA-Z0-9\-]+\.git$";

        var responseData = await GetSourcesLinkAsync();
        Assert.Matches(gitUrlPattern, responseData.SourcesLink.SourcesLink);
    }
    
    /// <summary>
    /// Checking the test format for json
    /// </summary>
    [Fact]
    public async Task GetSourcesLink_CheckTypeResponse()
    {
        var response = await _client.GetAsync("/api/SiteInfo/GetSourcesLink");
        
        response.EnsureSuccessStatusCode();
        
        Assert.NotNull(response);
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
    }

    #endregion

    #region Helpers

    #region Backend version

    /// <summary>
    /// A repository of options for what might come
    /// </summary>
    public static IEnumerable<object[]> GetVersionsData()
    {
        return new List<object[]>
        {
            new object[] { "0.0.2" },
            new object[] { "0.0.3" },
            new object[] { "" },
            new object[] { "0.0.2   " },
            new object[] { "   0.0.2" },
            new object[] { "   " },
            new object[] { "0.0    .2" },
            new object[] { null },
            new object[] { new string('a', 10000)},
            new object[] { new string('0', 10000)}
        };
    }

    /// <summary>
    /// Template code for getting version
    /// </summary>
    private async Task<BackendVersionResponse> GetBackendVersionAsync()
    {
        var response = await _client.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        response.EnsureSuccessStatusCode();
        
        var responseData = JsonSerializer.Deserialize<BackendVersionResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData);
        Assert.NotNull(responseData.BackendVersion);
        return responseData;
    }

    #endregion

    #region Sources link
    
    /// <summary>
    /// A repository of options for what might come
    /// </summary>
    public static IEnumerable<object[]> GetSourcesLinkData()
    {
        return new List<object[]>
        {
            new object[] { "https://github.com/FancyTheLion/OpenArtspaceGallery.git" },
            new object[] { "   https://github.com/FancyTheLion/OpenArtspaceGallery.git" },
            new object[] { "" },
            new object[] { "https://github.com/FancyTheLion/OpenArtspaceGallery.git   " },
            new object[] { "   https://github.com/FancyTheLion/OpenArtspaceGallery.git" },
            new object[] { "   " },
            new object[] { "https:   //github.com/FancyTheLion/   OpenArtspaceGallery.git" },
            new object[] { new string('a', 10000)},
            new object[] { new string('0', 10000)}
        };
    }
    
    private async Task<SourcesLinkResponse> GetSourcesLinkAsync()
    {
        var response = await _client.GetAsync("/api/SiteInfo/GetSourcesLink");
        
        response.EnsureSuccessStatusCode();
        
        var responseData = JsonSerializer.Deserialize<SourcesLinkResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData);
        Assert.NotNull(responseData.SourcesLink);
        return responseData;
    }

    #endregion

    #endregion
}