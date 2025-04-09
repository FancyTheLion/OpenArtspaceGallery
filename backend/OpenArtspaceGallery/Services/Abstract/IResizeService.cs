namespace OpenArtspaceGallery.Services.Abstract;

public interface IResizeService
{
    /// <summary>
    /// Method that changes the file size
    /// </summary>
    public Task<byte[]> ResizeImageAsync(byte[] content, int maxSize);
}