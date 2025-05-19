using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IImageProcessingDao
{
    /// <summary>
    /// Get image
    /// </summary>
    public Task<ImageDbo> GetImageByIdAsync(Guid imageId);
    
    /// <summary>
    /// Add image 
    /// </summary>
    Task<ImageDbo> AddImageAsync(ImageDbo image);
}