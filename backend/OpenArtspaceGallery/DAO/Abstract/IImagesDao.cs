using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IImagesDao
{
    /// <summary>
    /// Get image
    /// </summary>
    public Task<ImageDbo> GetImageByIdAsync(Guid imageId);
    
    /// <summary>
    /// Add image 
    /// </summary>
    Task<ImageDbo> AddImageAsync(ImageDbo image);
    
    /// <summary>
    /// Get images files IDs by sizes IDs 
    /// </summary>
    public Task<IReadOnlyDictionary<Guid, Guid?>> GetFilesIdsBySizesIdsAsync(Guid id, IReadOnlyCollection<Guid> sizesIds);

    /// <summary>
    /// Get images by album id
    /// </summary>
    public Task<IReadOnlyCollection<ImageDbo>> GetImagesByAlbumIdAsync(Guid albumId);

    /// <summary>
    /// Get thumbnails IDs for images
    /// </summary>
    public Task<IReadOnlyDictionary<Guid, Guid>> GetThumbnailsIdsForImagesAsync(IReadOnlyCollection<Guid> imageIds);
}