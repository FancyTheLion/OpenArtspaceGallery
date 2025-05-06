using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IImagesDao
{
    /// <summary>
    /// Add image 
    /// </summary>
    Task<ImageDbo> AddImageAsync(ImageDbo image);

    /*/// <summary>
    /// Adding to link table
    /// </summary>
    public Task<ImageFileDbo> AddImageFileAsync
    (
        Guid imageId,
        Guid fileId,
        Guid sizeId
    );*/
}