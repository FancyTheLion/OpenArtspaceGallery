using Microsoft.Extensions.Options;
using OpenArtspaceGallery.Constants;
using OpenArtspaceGallery.Models.Settings;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation.Hosted;

public class FilesStorageInitializer : IHostedService
{
    private readonly FilesStorageSettings _filesStorageSettings;
    
    public FilesStorageInitializer
    (
        IOptions<FilesStorageSettings> filesStorageSettings
    )
    {
        _filesStorageSettings = filesStorageSettings.Value;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _ = _filesStorageSettings.RootPath ?? throw new ArgumentException("Files storage root is not set!", nameof(_filesStorageSettings.RootPath));

        await CreateSubdirectoriesAsync(_filesStorageSettings.RootPath, FilesStorage.DirectoriesStructureDepth);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }
    
    private async Task CreateSubdirectoriesAsync(string currentPath, int depth)
    {
        if (depth == 0)
        {
            return;
        }
        
        for (var i = 0; i < FilesStorage.DirectoriesPerLevel; i++)
        {
            var directoryPath = Path.Combine(currentPath, i.ToString("x1"));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            CreateSubdirectoriesAsync(directoryPath, depth - 1);
        }
    }
}