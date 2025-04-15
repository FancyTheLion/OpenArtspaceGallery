using Microsoft.VisualBasic;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Services.Abstract;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Implementation;

public class ImageProcessingService : IImageProcessingService
{
    private readonly IFilesService _filesService;
    private readonly IResizeService _resizeService;
    private readonly IImagesSizesService _imagesSizesService;
    private readonly ILogger<FilesService> _logger;
    
    public ImageProcessingService
    (
        IFilesService filesService,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService,
        ILogger<FilesService> logger
    )
    {
        _filesService = filesService;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService;
        _logger = logger;
    }

    public async Task<FileInfo> UploadFileAsync(IFormFile file)
    {
        var fileInfo = await _filesService.UploadFileAsync(file);
        
        var sizes = await _imagesSizesService.GetListAsync();

        var resizesDictionary = await _resizeService.GenerateImagesSetAsync(fileInfo.Id, sizes);
        
        return fileInfo;
    }

    public async Task<FileForDownload> GetFileForDownloadAsync(Guid fileId)
    {
        var uploadedFile = await _filesService.GetFileForDownloadAsync(fileId);
        
        return uploadedFile;
    }
}