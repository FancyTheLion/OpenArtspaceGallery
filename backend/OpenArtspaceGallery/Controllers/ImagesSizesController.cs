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
    /// Get images sizes list 
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
    
    /// <summary>
    /// Delete image size 
    /// </summary>
    [HttpDelete]
    [Route("{sizeId:guid}")]
    public async Task<ActionResult> DeleteImageSizeAsync(Guid sizeId)
    {
        if (!await _imagesSizesService.IsImageSizeExistsByIdAsync(sizeId))
        {
            return NotFound();
        }

        await _imagesSizesService.DeleteImageSizeAsync(sizeId);

        return Ok();
    }

    /// <summary>
    /// Update image size fields: name, width, height
    /// </summary>
    [HttpPost]
    [Route("UpdateImageSizeById")]
    public async Task<ActionResult<UpdateImageSizeByIdResponse>> UpdateImageSizeByIdAsync(UpdateImageSizeByIdRequest request)
    {
        if (request == null)
        {
            return BadRequest("Update image size request mustn't be null!");
        }
        
        if (request.ImageSize == null)
        {
            return BadRequest("When updating image size, the size information must not be null.");
        }
        
        if (!await _imagesSizesService.IsImageSizeExistsByIdAsync(request.ImageSize.Id))
        {
            return NotFound();
        }

        try
        {
            return Ok
            (
                new UpdateImageSizeByIdResponse
                (
                    (await _imagesSizesService.UpdateImageSizeByIdAsync(request.ImageSize.ToModel())).ToDto()
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Is there an image size by name having different id
    /// </summary>
    [HttpPost]
    [Route("IsAnotherExistByName")]
    public async Task<ActionResult<ImageSizeNameExistenceResponse>> IsAnotherExistByNameAsync(AnotherImageSizeNameExistenceRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request mustn't be null!");
        }
        
        return Ok
        (
            new ImageSizeNameExistenceResponse
            (
                new ExistenceDto(await _imagesSizesService.IsAnotherExistByNameAsync(request.AnotherImageSizeExistenceByName.Id, request.AnotherImageSizeExistenceByName.Name))
            )
        );
    }

    /// <summary>
    /// Is there an image size by name by dimensions
    /// </summary>
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

    /// <summary>
    /// Is there an image size 
    /// </summary>
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
                new ExistenceDto(await _imagesSizesService.IsImageSizeExistsAsync(request.ImageSize.Name, request.ImageSize.Width, request.ImageSize.Height))
            )
        );
    }
}