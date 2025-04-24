using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Images;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IFilesService
{
    /// <summary>
    /// Upload file from form to OpenArtspaceGalleryStorage
    /// </summary>
    Task<FileInfo> UploadFileAsync(IFormFile file);
    
    /// <summary>
    /// Get file (for download)
    /// </summary>
    public Task<FileForDownload> GetFileForDownloadAsync(Guid fileId);
    
    Task<FileInfo> SaveFileAsync
    (
        string name,
        string type,
        byte[] content
    );
    
    /// <summary>
    /// Add image 
    /// </summary>
    Task<Image> AddImageAsync(string name, string? description, Guid albumId);
}