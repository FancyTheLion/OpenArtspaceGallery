using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Images;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IImageProcessingService
{
    /// <summary>
    /// Add image 
    /// </summary>
    public Task<Image> AddImageAsync(Image image, Guid sourceFileId);
}