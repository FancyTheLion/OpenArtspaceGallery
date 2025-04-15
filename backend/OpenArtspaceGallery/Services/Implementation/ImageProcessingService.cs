using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Services.Abstract;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Implementation;

public class ImageProcessingService : IImageProcessingService
{
    private readonly IFilesService _filesService;
    private readonly IResizeService _resizeService;
    
    public ImageProcessingService
    (
        IFilesService filesService,
        IResizeService resizeService
    )
    {
        _filesService = filesService;
        _resizeService = resizeService;
    }

    public async Task<FileInfo> UploadFileAsync(IFormFile file)
    {
        var fileInfo = await _filesService.UploadFileAsync(file);
        
        return fileInfo;
    }

    public async Task<FileForDownload> GetFileForDownloadAsync(Guid fileId)
    {
        var uploadedFile = await _filesService.GetFileForDownloadAsync(fileId);
        
        return uploadedFile;
    }
}