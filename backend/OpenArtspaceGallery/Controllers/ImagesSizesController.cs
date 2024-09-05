using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Responses;
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
    [Route("api/Files/Images/GetImagesSizesList")]
    public async Task<ActionResult<ImageSizesResponse>> GetImageSizesListAsync()
    {
        var imagesSizes = await _imagesSizesService.GetImagesSizesAsync();
        
        if (!imagesSizes.Any()) 
        {
            return NotFound();
        }

        var imagesSizesDtos = imagesSizes
            .Select(i => i.ToDto())
            .ToList();
        
        return Ok(new ImageSizesResponse(imagesSizesDtos));
    }
    
    /// <summary>
    /// Add Image Size entry
    /// </summary>
    [HttpPost]
    [Route("api/Files/Images/AddImageSizeAsync")]
    public async Task<ActionResult<AddImageSizeResponse>> AddImageSizeAsync(AddImageSizeRequest request)
    {
        var addImagesSizes = await _imagesSizesService.AddImageSizeAsync();
        
        return Ok(new AddImageSizeResponse(addImagesSizes.ToDto()));
    }
}