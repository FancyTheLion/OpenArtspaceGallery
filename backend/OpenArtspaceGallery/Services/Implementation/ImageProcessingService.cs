using Microsoft.VisualBasic;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Images;
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

    public async Task<FileInfo> UploadFileAsync(IFormFile file)
    {
        var fileInfo = await _filesService.UploadFileAsync(file);
        
        var sizes = await _imagesSizesService.GetListAsync();

        var resizesDictionary = await _resizeService.GenerateImagesSetAsync(fileInfo.Id, sizes);
        
        return fileInfo;
    }
    
    public async Task<Image> AddImageAsync(string name, string description, Guid albumId, Guid fileId, Guid sizeId)
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
    }
    

}