using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IFilesDao
{
    #region Create

    /// <summary>
    /// Create new file
    /// </summary>
    Task<FileDbo> CreateFileAsync(FileDbo file);
    
    /// <summary>
    /// Add image
    /// </summary>
    Task<ImageDbo> AddImageAsync(ImageDbo image);

    /// <summary>
    /// Adding a dimension to a link table
    /// </summary>
    Task<ImageFileDbo> AddImageFileAsync(ImageFileDbo imageFile);
    
    #endregion

    #region Get

    /// <summary>
    /// Get file type by MIME type
    /// </summary>
    public Task<FileTypeDbo> GetFileTypeByMimeTypeAsync(string mimeType);
    
    /// <summary>
    /// Get only file type ID by MIME type
    /// </summary>
    public Task<Guid?> GetFileTypeIdByMimeTypeAsync(string mimeType);

    /// <summary>
    /// Get file metadata from DB
    /// </summary>
    /// <param name="fileId"></param>
    public Task<FileDbo?> GetFileMetadataAsync(Guid fileId);

    #endregion
}