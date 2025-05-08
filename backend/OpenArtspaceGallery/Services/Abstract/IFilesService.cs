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
    /// Get file metadata
    /// </summary>
    public Task<FileMetadata> GetFileMetadataAsync(Guid fileId);
    
    /// <summary>
    /// Get file (for download)
    /// </summary>
    public Task<FileForDownload> GetFileForDownloadAsync(Guid fileId);
    
    /// <summary>
    /// 
    /// </summary>
    Task<FileInfo> SaveFileAsync
    (
        string name,
        string type,
        byte[] content
    );
}