using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class FolderInitializerService : IFolderInitializerService
{
    public Task CreateFolderStructure(string currentPath, int depth)
    {
        if (depth == 0) return Task.CompletedTask;
        
        for (int i = 0; i < 16; i++)
        {
            string folderName = i.ToString("x1");
            string folderPath = Path.Combine(currentPath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            CreateFolderStructure(folderPath, depth - 1);
        }
        
        return Task.CompletedTask;
    }
}