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
    
    public ImagesController
    (
        IImageProcessingService imageProcessingService
    )
    {
        _imageProcessingService = imageProcessingService;
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
}