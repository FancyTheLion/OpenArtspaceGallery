using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using OpenArtspaceGallery.Tests.Helpers;

namespace OpenArtspaceGallery.Tests;

public class TestsFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public const string SettingsFile = "appsettings.Testing.json";
    
    public readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile(SettingsFile, optional: false, reloadOnChange: true)
        .Build();
    
    public readonly HttpClient HttpClient;
    
    public readonly ImagesSizesHelper ImagesSizesHelper;

    public TestsFactory()
    {
        HttpClient = CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        ImagesSizesHelper = new ImagesSizesHelper(Configuration);
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}