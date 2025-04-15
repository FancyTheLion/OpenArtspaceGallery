using OpenArtspaceGallery.Models.Files;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IImageProcessingService
{
    /// <summary>
    /// Upload file
    /// </summary>
    public Task<FileInfo> UploadFileAsync(IFormFile file);

    /// <summary>
    /// Get file (for download)
    /// </summary>
    public Task<FileForDownload> GetFileForDownloadAsync(Guid fileId);
}