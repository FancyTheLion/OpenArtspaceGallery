using Microsoft.Extensions.Options;
using OpenArtspaceGallery.Models.Settings;

namespace OpenArtspaceGallery.Helpers.Validators;

public static class AlbumValidator
{
    
    // Connect appsettings.json
    /*private readonly AlbumSettings _albumSettings;

    public AlbumValidator(IOptions<AlbumSettings> albumSettings)
    {
        _albumSettings = albumSettings.Value; 
    }*/


    public static bool Validate(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length < 200;
    }
}