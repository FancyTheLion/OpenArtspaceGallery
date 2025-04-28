using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

public class ImagesController : ControllerBase
{
    private readonly IFilesService _filesService;
    private readonly IResizeService _resizeService;
    private readonly IImagesSizesService _imagesSizesService;
    private readonly IImageProcessingService _imageProcessingService;
    
    public ImagesController
    (
        IFilesService filesService,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService,
        IImageProcessingService imageProcessingService
    )
    {
        _filesService = filesService;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService;
        _imageProcessingService = imageProcessingService;
    }

    /// <summary>
    /// Add image
    /// </summary>
    [Route("AddImage")]
    [HttpPost]
    public async Task<ActionResult<Guid>> AddImage(AddImageRequest image)
    {
        var file = await _filesService.GetFileForDownloadAsync(image.AddImage.SourceFileId);

        if (file == null)
        {
            return NotFound();
        }

        var sizes = await _imagesSizesService.GetListAsync();
        
        var mainSize = sizes.First();
        
        var imageResult = await _imageProcessingService.AddImageAsync
        (
            image.AddImage.Name,
            image.AddImage.Description,
            image.AddImage.AlbumId,
            image.AddImage.SourceFileId,
            mainSize.Id
        );
        
        var resizedImages = await _resizeService.GenerateImagesSetAsync(image.AddImage.SourceFileId, sizes);

        return Ok(imageResult.Id);
    }
}