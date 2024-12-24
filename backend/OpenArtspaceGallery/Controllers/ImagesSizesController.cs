using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.DTOs.Shared;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/ImagesSizes")]
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
    [Route("GetImagesSizesList")]
    public async Task<ActionResult<ImagesSizesResponse>> GetImageSizesListAsync()
    {
        var imagesSizes = await _imagesSizesService.GetImagesSizesAsync();

        var imagesSizesDtos = imagesSizes
            .Select(i => i.ToDto())
            .ToList();
        
        return Ok(new ImagesSizesResponse(imagesSizesDtos));
    }
    
    /// <summary>
    /// Add Image Size entry
    /// </summary>
    [HttpPost]
    [Route("AddImageSize")]
    public async Task<ActionResult<AddImageSizeResponse>> AddImageSizeAsync(AddImageSizeRequest request)
    {
        if (request == null)
        {
            return BadRequest("Add image size request mustn't be null!");
        }
        
        if (request.ImageSize == null)
        {
            return BadRequest("When adding a new image size, the size information must not be zero.");
        }
        
        try
        {
            return Ok
            (
                new AddImageSizeResponse
                (
                    (await _imagesSizesService.AddImageSizeAsync(request.ImageSize.ToModel())).ToDto()
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("{sizeId:guid}")]
    public async Task<ActionResult> DeleteImageSizeAsync(Guid sizeId)
    {
        if (!await _imagesSizesService.IsImageSizeExistsAsync(sizeId))
        {
            return NotFound();
        }

        await _imagesSizesService.DeleteImageSizeAsync(sizeId);

        return Ok();
    }

    [HttpPost]
    [Route("UpdateImageSize")]
    public async Task<ActionResult<UpdateImageSizeResponse>> UpdateImageSizeAsync(UpdateImageSizeRequest request)
    {
        if (request == null)
        {
            return BadRequest("Update image size request mustn't be null!");
        }
        
        if (request.ImageSize == null)
        {
            return BadRequest("When update image size, the size information must not be null.");
        }
        
        if (!await _imagesSizesService.IsImageSizeExistsAsync(request.ImageSize.Id))
        {
            return NotFound();
        }

        try
        {
            return Ok
            (
                new UpdateImageSizeResponse
                (
                    (await _imagesSizesService.UpdateImageSizeAsync(request.ImageSize.ToModel())).ToDto()
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("IsExistByName")]
    public async Task<ActionResult<ImageSizeNameExistenceResponse>> IsExistByNameAsync(ImageSizeNameExistenceRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request mustn't be null!");
        }
        
        return Ok
        (
            new ImageSizeNameExistenceResponse
            (
                new ExistenceDto(await _imagesSizesService.IsExistByNameAsync(request.ImageSizeName.Name))
            )
        );
    }

    [HttpPost]
    [Route("IsExistByDimensions")]
    public async Task<ActionResult<ImageSizeDimensionsExistenceResponse>> IsExistByDimensionsAsync(ImageSizeDimensionsExistenceRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request mustn't be null!");
        }
        
        return Ok
        (
            new ImageSizeDimensionsExistenceResponse
            (
                new ExistenceDto(await _imagesSizesService.IsExistByDimensionsAsync(request.ImageSizeDimensionsExistence.Width, request.ImageSizeDimensionsExistence.Height))
            )
        );
    }

    [HttpPost]
    [Route("IsImageSizeExists")]
    public async Task<ActionResult<ImageSizeExistenceResponse>> IsImageSizeExistsAsync(ImageSizeExistenceRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request mustn't be null!");
        }
        
        return Ok
        (
            new ImageSizeExistenceResponse
            (
                new ExistenceDto(await _imagesSizesService.IsImageSizeExistsAsync(request.ImageSizeExistence.Name, request.ImageSizeExistence.Width, request.ImageSizeExistence.Height))
            )
        );
    }
}