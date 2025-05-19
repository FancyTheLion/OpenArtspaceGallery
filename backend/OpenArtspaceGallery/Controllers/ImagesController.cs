using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.Models.API.Responses.Images;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/Images")]
public class ImagesController : ControllerBase
{
    private readonly IImageProcessingService _imageProcessingService;
    private readonly IFilesService _filesService;
    
    public ImagesController
    (
        IImageProcessingService imageProcessingService,
        IFilesService filesService
    )
    {
        _imageProcessingService = imageProcessingService;
        _filesService = filesService;
    }

    /// <summary>
    /// Add image
    /// </summary>
    [Route("Add")]
    [HttpPost]
    public async Task<ActionResult<AddImageResponse>> AddImageAsync(AddImageRequest request)
    {
        var result =
            await _imageProcessingService.AddImageAsync(request.Image.ToImageModel(), request.Image.SourceFileId);

        return Ok(new AddImageResponse(result.ToDto()));
    }

    /// <summary>
    /// Get image info
    /// </summary>
    [Route("{id:guid}")]
    [HttpGet]
    public async Task<ActionResult<ImageInfoResponse>> GetImageInfoAsync(Guid id)
    {
        var image = await _imageProcessingService.GetImageByIdAsync(id);
        if (image == null)
        {
            return NotFound();
        }

        var files = await _filesService.GetFilesWithSizesByImageIdAsync(id);
    
        var dto = await _imageProcessingService.ToDto(image, files);
        return Ok(new ImageInfoResponse(dto));
    }
}