namespace OpenArtspaceGallery.Infrastructure.FileStorage;

public class FileStorageHelper
{
    public static string GetFilePath(string rootPath, string fileName)
    {
        var parts = fileName.Split('-');

        if (parts.Length != 5)
        {
            throw new ArgumentException("Invalid GUID format.", nameof(fileName));
        }
        
        string firstLevel = GetHexFolder(parts[0]);
        string secondLevel = GetHexFolder(parts[1]);
        
        string fileDirectory = Path.Combine(rootPath, firstLevel, secondLevel);
        string fullPath = Path.Combine(fileDirectory, fileName);

        return fullPath;
    }

    private static string GetHexFolder(string part)
    {
        int sum = part.Sum(c => Convert.ToInt32(c.ToString(), 16));
        int folderIndex = sum % 16;
        return folderIndex.ToString("x1");
    }

}