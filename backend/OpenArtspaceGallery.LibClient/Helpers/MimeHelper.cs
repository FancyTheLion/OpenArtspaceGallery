namespace OpenArtspaceGallery.LibClient.Helpers;

public static class MimeHelper
{
    /// <summary>
    /// File extension -> MIME type
    /// </summary>
    private static readonly Dictionary<string, string> ExtensionsToMimeTypes = new Dictionary<string, string>()
    {
        { "gif", "image/gif" },
        { "jpeg", "image/jpeg" },
        { "jpg", "image/jpeg" },
        { "png", "image/png" },
        { "bmp", "image/bmp" },
        { "webp", "image/webp" },
        { "x-icon", "image/x-icon" },
        { "svg+xml", "image/svg+xml" },
        { "heic", "image/heic" }
    }; 
    
    public static string GetMimeTypeByExtension(string filePath)
    {
        var ext = Path.GetExtension(filePath)
            .ToLowerInvariant()
            .Substring(1);

        if (!ExtensionsToMimeTypes.TryGetValue(ext, out var mimeType))
        {
            throw new NotSupportedException($"Extension {ext} is not supported.");
        }

        return mimeType;
    }
}