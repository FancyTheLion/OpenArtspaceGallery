using OpenArtspaceGallery.DAO.Constants;

namespace OpenArtspaceGallery.Helpers.Files.Images;

public static class ImageTypeHelper
{
    private static readonly string[] AllowedImageMimeTypes = new[]
    {
        MimeTypes.TypeMimeBmp,
        MimeTypes.TypeMimeJpeg,
        MimeTypes.TypeMimePng,
        MimeTypes.TypeMimeTiff,
        MimeTypes.TypeMimeSvg,
        MimeTypes.TypeMimeHeic,
        MimeTypes.TypeMimeGif,
        MimeTypes.TypeMimeIco,
        MimeTypes.TypeMimeWebP
    };

    /// <summary>
    /// Checks if the MIME type is one of the supported image types
    /// </summary>
    /// <returns>True if this is an image, otherwise False</returns>
    public static bool IsImageMimeType(string mimeType)
    {
        if (string.IsNullOrWhiteSpace(mimeType))
        {
            return false;            
        }

        return AllowedImageMimeTypes.Contains(mimeType, StringComparer.OrdinalIgnoreCase);
    }
}