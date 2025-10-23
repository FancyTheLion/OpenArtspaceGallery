using OpenArtspaceGallery.Models.API.DTOs.Albums;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.ConsoleClient;

public class Application
{
    private readonly ISiteInfoClient _siteInfoClient;
    private readonly IAlbumsClient _albumsClient;

    public Application
    (
        ISiteInfoClient siteInfoClient,
        IAlbumsClient albumsClient
    )
    {
        _siteInfoClient = siteInfoClient;
        _albumsClient = albumsClient;
    }
    
    public async Task RunAsync()
    {
        Console.WriteLine($"Backend version: { (await _siteInfoClient.GetBackendVersionAsync()).BackendVersion.Version }");
        
        // TODO: Create an album with random name
        var request = new NewAlbumRequest()
        {
            AlbumToAdd = new NewAlbumDto()
            {
                Name = $"Album {Guid.NewGuid()}",
                ParentId = null
            }
        };

        var response = await _albumsClient.CreateAlbumAsync(request);

        var album = response.NewAlbum;
        
        Console.WriteLine($"New album created: {album.Name}");
        
        
        
        
    }
}