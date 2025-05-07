using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IImageProcessingDao
{
    /// <summary>
    /// Get original image size
    /// </summary>
    Task<ImageSizeDbo?> GetImageSizeOriginalAsync();
    
    /// <summary>
    /// Get information about a file
    /// </summary>
    Task<FileDbo?> GetFileMetadataAsync(Guid fileId);
    
    /// <summary>
    /// Add image 
    /// </summary>
    Task<ImageDbo> AddImageAsync(ImageDbo image);
}