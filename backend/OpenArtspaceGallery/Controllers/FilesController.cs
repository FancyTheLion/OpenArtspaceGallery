using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFilesService _filesService;

    public FilesController
    (
        IFilesService filesService
    )
    {
        _filesService = filesService;
    }

    /// <summary>
    /// Get Images Sizes List Async
    /// </summary>
    [HttpGet]
    [Route("api/Files/Images/GetImagesSizesList")]
    public async Task<ActionResult<ImageSizesResponse>> GetImageSizesListAsync()
    {
        var imagesSizes = await _filesService.GetImagesSizesAsync();

        var imagesSizesDtos = imagesSizes
            .Select(i => i.ToDto())
            .ToList();
        
        return Ok(new ImageSizesResponse(imagesSizesDtos));
    }
}