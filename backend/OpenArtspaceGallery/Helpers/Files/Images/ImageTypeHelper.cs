using OpenArtspaceGallery.DAO.Constants;
using OpenArtspaceGallery.DAO.Constants.MimeTypes;

namespace OpenArtspaceGallery.Helpers.Files.Images;

public static class ImageTypeHelper
{
    private static readonly string[] AllowedImageMimeTypes = new[]
    {
        MimeTypes.Png.MimeType,
        MimeTypes.Jpeg.MimeType,
        MimeTypes.Bmp.MimeType,
        MimeTypes.Tiff.MimeType,
        MimeTypes.Svg.MimeType,
        MimeTypes.Heic.MimeType,
        MimeTypes.Gif.MimeType,
        MimeTypes.Ico.MimeType,
        MimeTypes.WebP.MimeType
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