using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IResizeService
{
    /// <summary>
    /// Method that changes the file size
    /// </summary>
    public Task<byte[]> ResizeImageAsync(byte[] content, int maxSize);

    /// <summary>
    /// Resize original image to images set of required sizes
    /// </summary>
    /// <param name="sourceFileId">Original file ID</param>
    /// <param name="sizes">Sizes list</param>
    /// <returns>Dictionary, where key is image size ID and value is resized image ID</returns>
    public Task<IReadOnlyDictionary<Guid, FileInfo>> GenerateImagesSetAsync
    (
        Guid sourceFileId,
        IReadOnlyCollection<ImageSize> sizes
    );
}