using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Files.Images;
using OpenArtspaceGallery.Models.Images;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class ImagesService : IImagesService
{
    private readonly IFilesService _filesService;
    private readonly IImagesDao _imagesDao;
    private readonly IResizeService _resizeService;
    private readonly IImagesSizesService _imagesSizesService;
    
    public ImagesService
    (
        IFilesService filesService,
        IImagesDao imagesDao,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService
    )
    {
        _filesService = filesService;
        _imagesDao = imagesDao;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService;
    }


    public async Task<Image> GetImageByIdAsync(Guid imageId)
    {
        var dbo = await _imagesDao.GetImageByIdAsync(imageId);
        return dbo != null ? Image.FromDbo(dbo) : null;
    }

    public async Task<Image> AddImageAsync(Image image, Guid sourceFileId)
    {
        var imageFile = await _filesService.GetFileForDownloadAsync(sourceFileId);

        if (image.AlbumId == Guid.Empty)
        {
            throw new ArgumentException($"Album with ID: { image.AlbumId } not found.", nameof(sourceFileId));
        }
        
        if (imageFile.Type.MimeType == null)
        {
            throw new ArgumentException($"File with { sourceFileId } not found.", nameof(sourceFileId));
        }

        if (!ImageTypeHelper.IsImageMimeType(imageFile.Type.MimeType))
        {
            throw new ArgumentException("This file is not an image.", nameof(sourceFileId));
        }

        var imagesSizes = (await _imagesSizesService.GetListAsync())
            .Where(i => i.Type != ImagesSizesTypes.Original)
            .ToList();

        var resizedImages = await _resizeService.GenerateImagesSetAsync(sourceFileId, imagesSizes);
        
        var imageFiles = new List<ImageFileDbo>();
        
        // Original size
        imageFiles.Add
        (
            new ImageFileDbo
            {
                File = new FileDbo() { Id = sourceFileId },
                Size = new ImageSizeDbo() { Id = ImagesSizes.Original.Id }
            }
        );
        
        imageFiles.AddRange
        (
            resizedImages.Select(ri => new ImageFileDbo()
            {
                Size = new ImageSizeDbo() { Id = ri.Key },
                File = new FileDbo() {  Id = ri.Value.Id},
            })
        );

        var dbo = new ImageDbo()
        {
            Id = Guid.Empty,
            Name = image.Name,
            Description = image.Description,
            Album = new AlbumDbo() { Id = image.AlbumId },
            CreationTime = DateTime.UtcNow,
            Files = imageFiles
        };
        
        return Image.FromDbo(await _imagesDao.AddImageAsync(dbo));
    }

    public async Task<IReadOnlyDictionary<Guid, Guid?>> GetFilesIdsBySizesIdsAsync(Guid id, IReadOnlyCollection<Guid> sizesIds)
    {
        _ = sizesIds ?? throw new ArgumentNullException(nameof(sizesIds), "Size IDs must not be null!");
        
        return await _imagesDao.GetFilesIdsBySizesIdsAsync(id, sizesIds);
    }

    public async Task<IReadOnlyCollection<Image>> GetImagesByAlbumIdAsync(Guid albumId)
    {
        var images = await _imagesDao.GetImagesByAlbumIdAsync(albumId);
        
        return images
            .Select(Image.FromDbo)
            .ToList();
    }

    public async Task<IReadOnlyDictionary<Guid, Guid>> GetThumbnailsIdsForImagesAsync(IReadOnlyCollection<Guid> imageIds)
    {
        return await _imagesDao.GetThumbnailsIdsForImagesAsync(imageIds);
    }
}