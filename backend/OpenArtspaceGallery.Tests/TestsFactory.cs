using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace OpenArtspaceGallery.Tests;

public class TestsFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public const string SettingsFile = "appsettings.Testing.json";
    
    public readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile(SettingsFile, optional: false, reloadOnChange: true)
        .Build();
    
    public readonly HttpClient HttpClient;

    public TestsFactory()
    {
        HttpClient = CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}