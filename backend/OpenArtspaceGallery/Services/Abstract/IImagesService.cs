using OpenArtspaceGallery.Models.Images;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IImagesService
{
    /// <summary>
    /// Get image
    /// </summary>
    public Task<Image> GetImageByIdAsync(Guid imageId);
    
    /// <summary>
    /// Add image 
    /// </summary>
    public Task<Image> AddImageAsync(Image image, Guid sourceFileId);
    
    /// <summary>
    /// Get images files IDs by sizes IDs 
    /// </summary>
    public Task<IReadOnlyDictionary<Guid, Guid?>> GetFilesIdsBySizesIdsAsync(Guid id, IReadOnlyCollection<Guid> sizesIds);

    /// <summary>
    /// Get images by album id
    /// </summary>
    public Task<IReadOnlyCollection<Image>> GetImagesByAlbumIdAsync(Guid albumId);

    /// <summary>
    /// Get thumbnails ids for images
    /// </summary>
    public Task<IReadOnlyDictionary<Guid, Guid>> GetThumbnailsIdsForImagesAsync(IReadOnlyCollection<Guid> imageIds);

}