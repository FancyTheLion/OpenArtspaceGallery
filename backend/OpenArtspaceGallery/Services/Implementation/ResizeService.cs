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

    public async Task<IReadOnlyDictionary<Guid, FileInfo>> GenerateImagesSetAsync(Guid sourceFileId, IReadOnlyCollection<ImageSize> sizes, byte[] content)
    {
        var mimeType = await _filesDao.GetMimeTypeByFileIdAsync(sourceFileId);
        
        var result = new Dictionary<Guid, FileInfo>();
        
        foreach (var size in sizes)
        {
            var resizedContent = await ResizeImageToFixedSizeAsync(content, size.Width, size.Height);
            
            var newFileId = Guid.NewGuid();
            var newFileName = $"{Path.GetFileNameWithoutExtension("image")}_{size.Name}_{size.Width}x{size.Height}.{mimeType}";
            
            var fileInfo = new FileInfo(newFileId, newFileName);
            result[size.Id] = fileInfo;
        }
        
        return result;
    }
}