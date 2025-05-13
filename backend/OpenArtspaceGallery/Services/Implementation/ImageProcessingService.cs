using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
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
    private readonly IImageProcessingDao _imageProcessingDao;
    private readonly IResizeService _resizeService;
    private readonly IImagesSizesService _imagesSizesService;
    
    public ImageProcessingService
    (
        IFilesService filesService,
        IImageProcessingDao imageProcessingDao,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService
    )
    {
        _filesService = filesService;
        _imageProcessingDao = imageProcessingDao;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService;
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

        var listImageSizes = await _imagesSizesService.GetListAsync();

        var resizedImages = await _resizeService.GenerateImagesSetAsync(sourceFileId, listImageSizes);
        
        var imageFiles = new List<ImageFileDbo>();
        
        imageFiles.Add
            (
                new ImageFileDbo
                {
                    File = new FileDbo() { Id = sourceFileId },
                    Size = new ImageSizeDbo() { Id = ImagesSizes.Original.Id }
                }
            );
        
        foreach (var kvp in resizedImages)
        {
            var sizeId = kvp.Key;
            var fileInfo = kvp.Value;

            var imageFileDbo = new ImageFileDbo
            {
                File = new FileDbo { Id = fileInfo.Id },
                Size = new ImageSizeDbo { Id = sizeId }
            };

            imageFiles.Add(imageFileDbo);
        }

        var dbo = new ImageDbo()
        {
            Id = Guid.Empty,
            Name = image.Name,
            Description = image.Description,
            Album = new AlbumDbo() { Id = image.AlbumId },
            CreationTime = DateTime.UtcNow,
            Files = imageFiles
        };
        
        return Image.FromDbo(await _imageProcessingDao.AddImageAsync(dbo));
    }
}