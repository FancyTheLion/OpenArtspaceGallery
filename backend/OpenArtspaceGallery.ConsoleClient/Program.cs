using Microsoft.Extensions.DependencyInjection;
using OpenArtspaceGallery.WebClient.Clients.Abstract;
using OpenArtspaceGallery.WebClient.Clients.Implementations;

namespace OpenArtspaceGallery.ConsoleClient;

class Program
{
    static async Task Main(string[] args)
    {
        #region DI
        
        var serviceCollection = new ServiceCollection();
        
        #region Typed HTTP clients

        serviceCollection.AddHttpClient<SiteInfoClient>();
        serviceCollection.AddHttpClient<AlbumsClient>();

        #endregion
        
        #region Singletons

        serviceCollection.AddSingleton<Application>();
        
        serviceCollection.AddSingleton<ISiteInfoClient, SiteInfoClient>();
        serviceCollection.AddSingleton<IAlbumsClient, AlbumsClient>();
        
        #endregion

        var serviceProvider = serviceCollection.BuildServiceProvider();

        #endregion
        
        var app = serviceProvider.GetService<Application>();
        await app.RunAsync();
    }
}