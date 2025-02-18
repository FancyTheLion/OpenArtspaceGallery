using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Models.API.DTOs.Shared;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Requests.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.Responses.ImagesSizes;
using OpenArtspaceGallery.Models.API.Responses.Shared;
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
    [Route("GetList")]
    public async Task<ActionResult<ImagesSizesResponse>> GetListAsync()
    {
        var imagesSizes = await _imagesSizesService.GetListAsync();

        var imagesSizesDtos = imagesSizes
            .Select(i => i.ToDto())
            .ToList();
        
        return Ok(new ImagesSizesResponse(imagesSizesDtos));
    }

    /// <summary>
    /// Get image size info
    /// </summary>
    [HttpGet]
    [Route("GetInfo")]
    public async Task<ActionResult<ImageSizeResponse>> GetInfoAsync(Guid id)
    {
        var imageSize = await _imagesSizesService.GetImageSizeByIdAsync(id);
    
        if (imageSize == null)
        {
            return NotFound($"Размер изображения с ID {id} не найден.");
        }

        return Ok(new ImageSizeResponse(imageSize.ToDto()));
    }

    /// <summary>
    /// Add Image Size entry
    /// </summary>
    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<AddImageSizeResponse>> AddAsync(AddImageSizeRequest request)
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
                    (await _imagesSizesService.AddAsync(request.ImageSize.ToModel())).ToDto()
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
    public async Task<ActionResult> DeleteAsync(Guid sizeId)
    {
        if (!await _imagesSizesService.IsExistsByIdAsync(sizeId))
        {
            return NotFound();
        }

        await _imagesSizesService.DeleteAsync(sizeId);

        return Ok();
    }

    /// <summary>
    /// Update image size fields: name, width, height
    /// </summary>
    [HttpPost]
    [Route("UpdateById")]
    public async Task<ActionResult<UpdateImageSizeByIdResponse>> UpdateByIdAsync(UpdateImageSizeByIdRequest request)
    {
        if (request == null)
        {
            return BadRequest("Update image size request mustn't be null!");
        }
        
        if (request.ImageSize == null)
        {
            return BadRequest("When updating image size, the size information must not be null.");
        }
        
        if (!await _imagesSizesService.IsExistsByIdAsync(request.ImageSize.Id))
        {
            return NotFound();
        }

        try
        {
            return Ok
            (
                new UpdateImageSizeByIdResponse
                (
                    (await _imagesSizesService.UpdateByIdAsync(request.ImageSize.ToModel())).ToDto()
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
                new ExistenceDto(await _imagesSizesService.IsExistByNameAsync(request.ImageSizeExistenceByName.Name))
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
    [Route("IsExists")]
    public async Task<ActionResult<ExistenceResponse>> IsExistsAsync(ImageSizeExistenceRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request mustn't be null!");
        }
        
        return Ok
        (
            new ExistenceResponse
            (
                new ExistenceDto(await _imagesSizesService.IsImageSizeExistsAsync(request.ImageSize.Name, request.ImageSize.Width, request.ImageSize.Height))
            )
        );
    }
}