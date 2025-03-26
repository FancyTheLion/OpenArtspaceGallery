using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Infrastructure.FileStorage;

public class FolderInitializer : IHostedService
{
    private readonly string _rootPath;
    private readonly IFolderInitializerService _folderInitializerService;

    public FolderInitializer(IConfiguration config, IFolderInitializerService folderInitializerService)
    {
        _rootPath = config["FileStorageSettings:RootPath"] ?? "FileStorage";
        _folderInitializerService = folderInitializerService;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _folderInitializerService.CreateFolderStructure(_rootPath, depth: 5);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}