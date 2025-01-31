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
    
    [Fact]
    public async Task GetBackendVersion_ShouldReturnCorrectVersion()
    {
        // Arrange
        var expectedVersion = _expectedVersion;

        var responseData = await GetBackendVersionAsync();
        Assert.Equal(expectedVersion, responseData.BackendVersion.Version);
    }

    [Theory]
    [MemberData(nameof(GetVersionsData))]

    public async Task GetBackendVersion_TestingProcessingVariousInputData(string version)
    {
        var responseData = await GetBackendVersionAsync();
    }

    [Fact]
    public async Task GetBackendVersion_CheckTypeVersion()
    {
        var responseData = await GetBackendVersionAsync();
        Assert.IsType<string>(responseData.BackendVersion.Version);
    }

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
            new object[] { "0.0    .2" }
        };
    }

    private async Task<BackendVersionResponse> GetBackendVersionAsync()
    {
        var response = await _client.GetAsync("/api/SiteInfo/GetBackendVersion");
        
        response.EnsureSuccessStatusCode();
        
        var responseData = JsonSerializer.Deserialize<BackendVersionResponse>(await response.Content.ReadAsStringAsync());
        
        Assert.NotNull(responseData);
        Assert.NotNull(responseData.BackendVersion);
        return responseData;
    }
}