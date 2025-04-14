using OpenArtspaceGallery.Models;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IResizeService
{
    /// <summary>
    /// Method that changes the file size
    /// </summary>
    public Task<byte[]> ResizeImageAsync(byte[] content, int maxSize);

    /// <summary>
    /// Resize to fixed size
    /// </summary>
    public Task<byte[]> ResizeImageToFixedSizeAsync(byte[] content, int width, int height);

    /// <summary>
    /// Resize original image to images set of required sizes
    /// </summary>
    /// <returns>Dictionary, where key is image size ID and value is resized image ID</returns>
    public Task<IReadOnlyDictionary<Guid, byte[]>> GenerateImagesSetAsync
    (
        Guid sourceFileId,
        IReadOnlyCollection<ImageSize> sizes,
        byte[] originalContent
    );
}