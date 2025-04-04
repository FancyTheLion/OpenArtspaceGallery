using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Models.Files;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IFilesService
{
    /// <summary>
    /// Upload file from form to OpenArtspaceGalleryStorage
    /// </summary>
    Task<FileInfoDto> UploadFileAsync(IFormFile file);

    /// <summary>
    /// Get file (for download)
    /// </summary>
    public Task<FileModel> GetFileAsync(Guid fileId);
}