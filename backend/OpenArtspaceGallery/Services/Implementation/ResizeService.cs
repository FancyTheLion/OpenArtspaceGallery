using ImageMagick;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Services.Abstract;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Implementation;

public class ResizeService : IResizeService
{
    private readonly IFilesDao _filesDao;

    public ResizeService
    (
        IFilesDao filesDao
    )
    {
        _filesDao = filesDao;
    }

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

    public async Task<byte[]> ResizeImageToFixedSizeAsync(byte[] content, int width, int height)
    {
        using (var imagesCollection = new MagickImageCollection())
        {
            imagesCollection.Read(content);

            foreach (var image in imagesCollection)
            {
                image.Resize((uint)width, (uint)height);
            }

            using (var saveStream = new MemoryStream())
            {
                await imagesCollection.WriteAsync(saveStream);
                return saveStream.ToArray();
            }
        }
    }

    public async Task<IReadOnlyDictionary<Guid, byte[]>> GenerateImagesSetAsync(Guid sourceFileId, IReadOnlyCollection<ImageSize> sizes, byte[] originalContent)
    {
        var result = new Dictionary<Guid, byte[]>();

        foreach (var size in sizes)
        {
            var resizedContent = await ResizeImageToFixedSizeAsync(originalContent, size.Width, size.Height);
            result[size.Id] = resizedContent;
        }

        return result;
    }
}