using OpenArtspaceGallery.Models.API.DTOs.Files;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IFilesService
{
    /// <summary>
    /// Upload file from form to database
    /// </summary>
    Task<FileInfoDto> UploadFileAsync(IFormFile file);
}