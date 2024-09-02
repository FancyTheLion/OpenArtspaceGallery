using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IFilesService
{
    /// <summary>
    /// Getting the images sizes
    /// </summary>
    Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync();
}