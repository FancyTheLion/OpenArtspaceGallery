using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.Responses.SiteInfo;
using OpenArtspaceGallery.Models.Settings;

namespace OpenArtspaceGallery.Tests.SiteInfo;

public class SiteInfoTests  : IClassFixture<TestsFactory<Program>>
{
    #region Constants

    private const string BackendVersionRegex = @"^\d+\.\d+\.\d+$";
    
    private const string SourcesLinkRegex = @"^https:\/\/github\.com\/[a-zA-Z0-9\-]+\/[a-zA-Z0-9\-]+\.git$";
    
    #endregion
    
    #region Initialization
    
    private readonly TestsFactory<Program> _factory;
    
    public SiteInfoTests(TestsFactory<Program> factory)
    {
        _factory = factory;
    }

    #endregion

    #region Backend version

    /// <summary>
    /// Testing the received version with the expected one
    /// </summary>
    [Fact]
    public async Task GetBackendVersion_ShouldReturnCorrectVersion()
    {
        Assert.Equal
        (
            _factory.Configuration["SiteInfoSettings:Version"] ?? throw new InvalidOperationException("BackendVersion not found in appsettings.Testing.json"),
            (await GetBackendVersionAsync()).BackendVersion.Version
        );
    }
    
    /// <summary>
    /// Checking backend version format
    /// </summary>
    [Fact]
    public async Task GetBackendVersion_CheckVersionFormat()
    {
        Assert.Matches
        (
            BackendVersionRegex,
            (await GetBackendVersionAsync()).BackendVersion.Version
        );
    }

    #endregion
    
    #region Sources link

    /// <summary>
    /// Checking the received sources link against the expected one
    /// </summary>
    [Fact]
    public async Task GetSourcesLink_ShouldReturnCorrectLink()
    {
        var responseData = await GetSourcesLinkAsync();
        Assert.Equal
        (
            _factory.Configuration["SiteInfoSettings:SourcesUrl"] ?? throw new InvalidOperationException($"SourcesLink not found in { TestsFactory<Program>.SettingsFile }"),
            (await GetSourcesLinkAsync()).SourcesLink.SourcesLink
        );
    }
    
    /// <summary>
    /// Checking the test url format
    /// </summary>
    [Fact]
    public async Task GetSourcesLink_CheckUrlExpectedFormat()
    {
        Assert.Matches
        (
            SourcesLinkRegex,
            (await GetSourcesLinkAsync()).SourcesLink.SourcesLink
        );
    }

    #endregion

    #region Helpers

    #region Backend version
    
    private async Task<BackendVersionResponse> GetBackendVersionAsync()
    {
        var response = await _factory.HttpClient.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        response.EnsureSuccessStatusCode();
        
        var responseData = JsonSerializer.Deserialize<BackendVersionResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData);
        Assert.NotNull(responseData.BackendVersion);
        
        return responseData;
    }

    #endregion

    #region Sources link
    
    private async Task<SourcesLinkResponse> GetSourcesLinkAsync()
    {
        var response = await _factory.HttpClient.GetAsync("/api/SiteInfo/GetSourcesLink");
        
        response.EnsureSuccessStatusCode();
        
        var responseData = JsonSerializer.Deserialize<SourcesLinkResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData);
        Assert.NotNull(responseData.SourcesLink);
        
        return responseData;
    }

    #endregion

    #endregion
}