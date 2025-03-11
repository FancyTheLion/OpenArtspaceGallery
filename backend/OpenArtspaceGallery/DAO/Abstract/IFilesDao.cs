using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;

namespace OpenArtspaceGallery.DAO.Abstract;

public interface IFilesDao
{
    #region Create

    /// <summary>
    /// Create new file
    /// </summary>
    Task CreateFileAsync(FileDbo file);

    #endregion

    #region Save

    public Task<FileTypeDbo> GetFileTypeByMimeTypeAsync(string mimeType);

    #endregion
}