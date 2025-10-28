using OpenArtspaceGallery.Models.API.DTOs.Albums;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.WebClient.Clients.Abstract;

namespace OpenArtspaceGallery.ConsoleClient;

public class Application
{
    private readonly ISiteInfoClient _siteInfoClient;
    private readonly IAlbumsClient _albumsClient;
    private readonly IFilesClient _filesClient;

    public Application
    (
        ISiteInfoClient siteInfoClient,
        IAlbumsClient albumsClient,
        IFilesClient filesClient
    )
    {
        _siteInfoClient = siteInfoClient;
        _albumsClient = albumsClient;
        _filesClient = filesClient;
    }
    
    public async Task RunAsync()
    {
        Console.WriteLine($"Backend version: { (await _siteInfoClient.GetBackendVersionAsync()).BackendVersion.Version }");
        
        // Create an album with random name
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

        var filePath = "/home/fancy/Projects/OpenArtspaceGalleryStorage/0/0/relaxing_sfw.png";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found");
            return;
        }

        var fileName = Path.GetFileName(filePath);
        var content = await File.ReadAllBytesAsync(filePath);
        var mimeType = GetMimeTypeByExtension(filePath);

        var uploaded = await _filesClient.UploadAsync(fileName, mimeType, content);
        
        Console.WriteLine($"File uploaded. ID: {uploaded.FileInfo.Id}");
    }
    
    private string GetMimeTypeByExtension(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();

        return ext switch
        {
            ".gif" => "image/gif",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            ".x-icon" => "image/x-icon",
            ".svg+xml" => "image/svg+xml",
            ".heic" => "image/heic",
            _ => "application/octet-stream"
        };
    }
}