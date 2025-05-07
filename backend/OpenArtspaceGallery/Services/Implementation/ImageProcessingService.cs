using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Files.Images;
using OpenArtspaceGallery.Models.Albums;
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
    private readonly IImageProcessingDao _imageProcessingDao;
    
    public ImageProcessingService
    (
        IFilesService filesService,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService,
        IImageProcessingDao imageProcessingDao
    )
    {
        _filesService = filesService;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService; 
        _imageProcessingDao = imageProcessingDao;
    }
    
    public async Task<Image> AddImageAsync(Image image, Guid sourceFileId)
    {
        var imageFile = await _filesService.GetFileForDownloadAsync(sourceFileId);

        if (imageFile.Type.MimeType == null)
        {
            throw new ArgumentException($"File with { sourceFileId } not found.", nameof(sourceFileId));
        }

        if (!ImageTypeHelper.IsImageMimeType(imageFile.Type.MimeType))
        {
            throw new ArgumentException("This file is not an image.", nameof(sourceFileId));
        }
        
        var originalSize = await _imageProcessingDao.GetImageSizeOriginalAsync();
        
        var metadata = await _imageProcessingDao.GetFileMetadataAsync(sourceFileId);
        
        var imageFileDbo = new ImageFileDbo()
        {
            Id = Guid.NewGuid(),
            File = metadata,
            Size = originalSize
        };

        var dbo = new ImageDbo()
        {
            Id = Guid.Empty,
            Name = image.Name,
            Description = image.Description,
            Album = new AlbumDbo() { Id = image.AlbumId },
            CreationTime = DateTime.UtcNow,
            Files = new List<ImageFileDbo>{ imageFileDbo }
        };
        
        var addedImage = await _imageProcessingDao.AddImageAsync(dbo);

        return addedImage.ToModel();
    }
}