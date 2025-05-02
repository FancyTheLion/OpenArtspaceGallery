using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Files.Images;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Images;
using OpenArtspaceGallery.Services.Abstract;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Implementation;

public class ImageProcessingService : IImageProcessingService
{
    private readonly IFilesService _filesService;
    private readonly IResizeService _resizeService;
    private readonly IImagesSizesService _imagesSizesService;
    private readonly IAlbumsDao _albumsDao;
    private readonly IImagesDao _imagesDao;
    
    public ImageProcessingService
    (
        IFilesService filesService,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService,
        IAlbumsDao albumsDao,
        IImagesDao imagesDao
    )
    {
        _filesService = filesService;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService; 
        _albumsDao = albumsDao;
        _imagesDao = imagesDao;
    }
    
    /*public async Task<Image> AddImageAsync(string name, string description, Guid albumId, Guid fileId, Guid sizeId)
    {
        var album = await _albumsDao.AttachAlbumByIdAsync(albumId);
        
        var imageDbo = new ImageDbo
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreationTime = DateTime.UtcNow,
            Album = album,
            Files = new List<ImageFileDbo>()
        };
        
        var createdImage = await _imagesDao.AddImageAsync(imageDbo);
        
        await _imagesDao.AddImageFileAsync(
            createdImage.Id,
            fileId,
            sizeId
        );
        
        return Image.FromDbo(createdImage);
    }*/
    
    public async Task<Image> AddImageAsync(Image image, Guid sourceFileId)
    {
        var imageFile = await _filesService.GetFileForDownloadAsync(sourceFileId);

        if (imageFile.Type.MimeType == null)
        {
            throw new ArgumentException($"File with { sourceFileId } not found.", nameof(sourceFileId));
        }
        
        // TODO: Check file type
        
        ImageTypeHelper.IsImageMimeType(imageFile.Type.MimeType);

        var dbo = new ImageDbo()
        {
            Id = Guid.Empty,
            Name = image.Name,
            Description = image.Description,
            Album = new AlbumDbo() { Id = image.AlbumId },
            CreationTime = DateTime.UtcNow,
            Files = new List<ImageFileDbo>()
        };
        
        var addedImage = await _imagesDao.AddImageAsync(dbo);

        return addedImage.ToModel();
    }
}