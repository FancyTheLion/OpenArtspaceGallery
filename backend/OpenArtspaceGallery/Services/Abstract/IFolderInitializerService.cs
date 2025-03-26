namespace OpenArtspaceGallery.Services.Abstract;

public interface IFolderInitializerService
{
    /// <summary>
    /// Create folder structure
    /// </summary>
    Task CreateFolderStructure(string currentPath, int depth);
}