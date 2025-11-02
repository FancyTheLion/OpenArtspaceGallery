using Microsoft.Extensions.DependencyInjection;
using OpenArtspaceGallery.WebClient.Clients.Abstract;
using OpenArtspaceGallery.WebClient.Clients.Implementations;

namespace OpenArtspaceGallery.ConsoleClient;

class Program
{
    static async Task<int> Main(string[] args)
    {
        #region DI
        
        var serviceCollection = new ServiceCollection();
        
        #region Singletons

        serviceCollection.AddSingleton<Application>();
        
        #endregion

        var serviceProvider = serviceCollection.BuildServiceProvider();

        #endregion
        
        var app = serviceProvider.GetService<Application>();
        return await app.RunAsync(args);
    }
}