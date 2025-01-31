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
    private readonly HttpClient _client;
    private readonly string _expectedVersion;
    
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
    }
    
    [Theory]
    [InlineData("0.0.2")]
    [InlineData("0.0.3")]
    [InlineData("")]
    [InlineData("0.0.2   ")]
    [InlineData("   0.0.2")]
    [InlineData("   ")]
    [InlineData("0.0    .2")]
    public async Task Get_GetBackendVersion_CheckVersion(string version)
    {
        // Arrange
        var expectedVersion = _expectedVersion;
        
        // Act
        var response = await _client.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        response.EnsureSuccessStatusCode();
        
        // Assert
        var responseData = JsonSerializer.Deserialize<BackendVersionResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); 
        Assert.NotNull(responseData.BackendVersion); 
        Assert.Equal(expectedVersion, responseData.BackendVersion.Version);
        Assert.IsType<string>(responseData.BackendVersion.Version);
    }

    [Fact]
    public async Task Get_GetBackendVersion_CheckTypeVersion()
    {
        // Act
        var response = await _client.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        response.EnsureSuccessStatusCode();
        
        // Assert
        var responseData = JsonSerializer.Deserialize<BackendVersionResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData); 
        Assert.NotNull(responseData.BackendVersion); 
        Assert.IsType<string>(responseData.BackendVersion.Version);
    }

}