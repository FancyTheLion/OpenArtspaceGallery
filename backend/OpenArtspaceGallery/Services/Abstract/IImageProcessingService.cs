using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Images;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IImageProcessingService
{
    /// <summary>
    /// Upload file
    /// </summary>
    public Task<FileInfo> UploadFileAsync(IFormFile file);

    /// <summary>
    /// Add image 
    /// </summary>
    public Task<Image> AddImageAsync(string name, string description, Guid albumId, Guid fileId, Guid sizeId);
}