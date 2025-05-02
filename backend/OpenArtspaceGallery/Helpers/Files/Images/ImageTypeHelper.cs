using OpenArtspaceGallery.DAO.Constants;

namespace OpenArtspaceGallery.Helpers.Files.Images;

public class ImageTypeHelper
{
    private static readonly string[] AllowedImageMimeTypes = new[]
    {
        Types.TypeMimeBmp,
        Types.TypeMimeJpeg,
        Types.TypeMimePng,
        Types.TypeMimeTiff,
        Types.TypeMimeSvg,
        Types.TypeMimeHeic,
        Types.TypeMimeGif,
        Types.TypeMimeIco,
        Types.TypeMimeWebP
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