using ImageMagick;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class ResizeService : IResizeService
{
    public async Task<byte[]> ResizeImageAsync(byte[] content, int maxSize)
    {
        using (var imagesCollection = new MagickImageCollection())
        {
            imagesCollection.Read(content);

            foreach (var image in imagesCollection)
            {
                var width = image.Width;
                var height = image.Height;

                var scale = (double)maxSize / Math.Max(width, height);
                var newWidth = width * scale;
                var newHeight = height * scale;

                image.Resize((uint)newWidth, (uint)newHeight);
            }

            using (var saveStream = new MemoryStream())
            {
                await imagesCollection.WriteAsync(saveStream);
                return saveStream.ToArray();
            }
        }
    }

    public Task<IReadOnlyDictionary<Guid, FileInfo>> GenerateImagesSetAsync(Guid sourceFileId, IReadOnlyCollection<ImageSize> sizes)
    {
        throw new NotImplementedException();
    }
}