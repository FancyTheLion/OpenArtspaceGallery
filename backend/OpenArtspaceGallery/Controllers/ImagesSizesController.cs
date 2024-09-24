using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
public class ImagesSizesController : ControllerBase
{
    private readonly IImagesSizesService _imagesSizesService;

    public ImagesSizesController
    (
        IImagesSizesService imagesSizesService
    )
    {
        _imagesSizesService = imagesSizesService;
    }

    /// <summary>
    /// Get Images Sizes List 
    /// </summary>
    [HttpGet]
    [Route("api/ImagesSizes/GetImagesSizesList")]
    public async Task<ActionResult<ImageSizesResponse>> GetImageSizesListAsync()
    {
        var imagesSizes = await _imagesSizesService.GetImagesSizesAsync();

        var imagesSizesDtos = imagesSizes
            .Select(i => i.ToDto())
            .ToList();
        
        return Ok(new ImageSizesResponse(imagesSizesDtos));
    }
    
    /// <summary>
    /// Add Image Size entry
    /// </summary>
    [HttpPost]
    [Route("api/ImagesSizes/AddImageSize")]
    public async Task<ActionResult<AddImageSizeResponse>> AddImageSizeAsync(AddImageSizeRequest request)
    {
        if (request == null)
        {
            return BadRequest("Add image size request mustn't be null!");
        }
        
        if (request.AddImageSize == null)
        {
            return BadRequest("When adding a new image size, the size information must not be zero.");
        }
        
        try
        {
            return Ok
            (
                new AddImageSizeResponse
                (
                    (await _imagesSizesService.AddImageSizeAsync(request.AddImageSize.ToModel())).ToDto()
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("api/ImagesSizes/{sizeId}")]
    public async Task<ActionResult> DeleteImageSizeAsync(Guid sizeId)
    {
        if (!await _imagesSizesService.IsImageSizeExistsAsync(sizeId))
        {
            return NotFound();
        }
        
        await _imagesSizesService.DeleteImageSizeAsync(sizeId);

        return Ok();
    }
}