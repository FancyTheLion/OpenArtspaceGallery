using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Files.Images;
using OpenArtspaceGallery.Models.Albums;
using OpenArtspaceGallery.Models.API.DTOs.Images;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Images;
using OpenArtspaceGallery.Models.ImagesSizes;
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


    public async Task<Image> GetImageByIdAsync(Guid imageId)
    {
        var dbo = await _imageProcessingDao.GetImageByIdAsync(imageId);
        return dbo != null ? Image.FromDbo(dbo) : null;
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
        
        return Image.FromDbo(await _imageProcessingDao.AddImageAsync(dbo));
    }

    public async Task<ImageInfoDto> ToDto(Image image, List<(FileInfo file, ImageSize size)> imageFiles)
    {
        return new ImageInfoDto
        {
            Id = image.Id,
            Name = image.Name,
            Description = image.Description,
            AlbumId = image.AlbumId,
            CreationTime = image.CreationTime,
            Files = imageFiles.Select(pair => new ImageFileInfoDto
                {
                    File = new FileWithSizeDto
                    {
                        Id = pair.file.Id,
                        Size = new ImageSizeShortDto
                        {
                            Id = pair.size.Id,
                            Name = pair.size.Name,
                            Type = (int)pair.size.Type
                        }
                    }
                }).ToList()
        };
    }
}