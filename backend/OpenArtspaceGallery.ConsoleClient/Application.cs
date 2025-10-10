using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.ConsoleClient;

public class Application
{
    private readonly ISiteInfoClient _siteInfoClient;

    public Application
    (
        ISiteInfoClient siteInfoClient
    )
    {
        _siteInfoClient = siteInfoClient;
    }
    
    public async Task RunAsync()
    {
        Console.WriteLine($"Backend version: { (await _siteInfoClient.GetBackendVersionAsync()).BackendVersion.Version }");
        
        // TODO: Create an album with random name
    }
}