using ImageMagick;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.Albums;
using OpenArtspaceGallery.Services.Abstract;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Implementation;

public class ResizeService : IResizeService
{
    private readonly IFilesService _filesService;
    private readonly ILogger<FilesService> _logger;

    public ResizeService
    (
        IFilesService filesService,
        ILogger<FilesService> logger
    )
    {
        _filesService = filesService;
        _logger = logger;
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

    public async Task<IReadOnlyDictionary<Guid, FileInfo>> GenerateImagesSetAsync(Guid sourceFileId, IReadOnlyCollection<ImageSize> sizes)
    {
        var sourceFile = await _filesService.GetFileForDownloadAsync(sourceFileId);
        
        var results = new Dictionary<Guid, FileInfo>();
        
        foreach (var size in sizes)
        {
                var resizedImage = await ResizeImageAsync(sourceFile.Content, Math.Max(size.Width, size.Height));
            
                var savedFile = await _filesService.SaveFileAsync(
                    $"{ Path.GetFileNameWithoutExtension(sourceFile.OriginalName)}_{size.Width}x{size.Height}{Path.GetExtension(sourceFile.OriginalName) }",
                    sourceFile.Type.MimeType,
                    resizedImage
                );

                results[size.Id] = savedFile;
        }

        return results;
    }
}