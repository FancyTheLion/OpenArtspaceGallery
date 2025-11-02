using System.CommandLine;
using OpenArtspaceGallery.LibClient.Helpers;
using OpenArtspaceGallery.Models.API.DTOs.Albums;
using OpenArtspaceGallery.Models.API.DTOs.Images;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.WebClient.Clients.Abstract;
using OpenArtspaceGallery.WebClient.Clients.Implementations;

namespace OpenArtspaceGallery.ConsoleClient;

public class Application
{
    private ISiteInfoClient _siteInfoClient;
    private IAlbumsClient _albumsClient;
    private IFilesClient _filesClient;
    private IImageClient _imageClient; // TODO: IImagesClient and so on
    
    public async Task<int> RunAsync(string[] args)
    {
        #region Command line options
        
        #region Server address
        
        Option<string> serverAddressCommandlineOption = new("--server")
        {
            Required = true,
            Description = "Gallery server to connect to."
        };
        
        #endregion
        
        var commandLineRootCommand = new RootCommand("Open Artspace Gallery Console Client");
        commandLineRootCommand.Options.Add(serverAddressCommandlineOption);
        
        #endregion
        
        #region Commandline parsing
        
        var commandLineParseResult = commandLineRootCommand.Parse(args);

        if (commandLineParseResult.Errors.Any())
        {
            Console.WriteLine("Wrong command line arguments!");

            Console.WriteLine(String.Join(Environment.NewLine, commandLineParseResult.Errors.Select(e => e.Message)));

            return 1;
        }

        var serverAddress = commandLineParseResult.GetRequiredValue(serverAddressCommandlineOption);
        
        #endregion
        
        #region Creating clients

        var baseAddressUri = new Uri(serverAddress);
        
        _siteInfoClient = new SiteInfoClient(new HttpClient(), baseAddressUri);
        _albumsClient = new AlbumsClient(new HttpClient(), baseAddressUri);
        _filesClient = new FilesClient(new HttpClient(), baseAddressUri);
        _imageClient = new ImageClient(new HttpClient(), baseAddressUri);
        
        #endregion
        
        Console.WriteLine($"Backend version: { (await _siteInfoClient.GetBackendVersionAsync()).BackendVersion.Version }");
        
        // Create an album with random name
        var albumRequest = new NewAlbumRequest()
        {
            AlbumToAdd = new NewAlbumDto()
            {
                Name = $"Album { Guid.NewGuid() }", // TODO: Remove hardcoded album name, use name from command line
                ParentId = null
            }
        };

        var albumResponse = await _albumsClient.CreateAlbumAsync(albumRequest);

        var album = albumResponse.NewAlbum;
        
        Console.WriteLine($"New album created: { album.Name }");

        // File upload
        var filePath = "/home/fancy/Projects/OpenArtspaceGalleryStorage/0/0/relaxing_sfw.png"; // TODO: Remove hardcoded path to file, use path from command line

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found");
            return 2;
        }

        var fileName = Path.GetFileName(filePath);
        var content = await File.ReadAllBytesAsync(filePath);
        var mimeType = MimeHelper.GetMimeTypeByExtension(filePath);

        var uploaded = await _filesClient.UploadAsync(fileName, mimeType, content);
        
        Console.WriteLine($"File uploaded. ID: {uploaded.FileInfo.Id}");
        
        // Create new image
        var imageRequest = new AddImageRequest()
        {
            Image = new AddImageDto()
            {
                Id = Guid.NewGuid(),
                Name = $"Image name {Guid.NewGuid()}",
                Description = $"Image description {Guid.NewGuid()}",
                AlbumId = album.Id,
                CreationTime = DateTime.Now,
                SourceFileId = uploaded.FileInfo.Id
            }
        };
        
        var imageResponse = await _imageClient.AddImageAsync(imageRequest);
        
        Console.WriteLine($"Image added. Name: { imageResponse.Image.Name }");

        return 0;
    }
}