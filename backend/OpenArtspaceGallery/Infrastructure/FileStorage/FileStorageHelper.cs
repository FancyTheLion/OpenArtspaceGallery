using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using OpenArtspaceGallery.Constants;
using OpenArtspaceGallery.Models.Settings;

namespace OpenArtspaceGallery.Infrastructure.FileStorage;

public class FileStorageHelper
{
    public static string GetFilePath(string rootPath, Guid fileId)
    {
        var fileIdAsString = fileId.ToString();
        var parts = fileIdAsString.Split(FilesStorage.GuidSeparator);
        
        var firstLevel = GetHexFolder(parts[0]);
        var secondLevel = GetHexFolder(parts[1]);
        
        var fileDirectory = Path.Combine(rootPath, firstLevel, secondLevel);
        var fullPath = Path.Combine(fileDirectory, fileIdAsString);

        return fullPath;
    }

    private static string GetHexFolder(string part)
    {
        return (Encoding.UTF8.GetBytes(part).Sum(b => b) % 16).ToString("x1");
    }
}