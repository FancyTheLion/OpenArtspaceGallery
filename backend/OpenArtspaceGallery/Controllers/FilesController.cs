using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Models.API.Responses.Files;
using OpenArtspaceGallery.Models.Settings;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/Files")]
public class FilesController : ControllerBase
{
    private readonly IImageProcessingService _imageProcessingService;
    
    public FilesController
    (
        IImageProcessingService imageProcessingService
    )
    {
        _imageProcessingService = imageProcessingService;
    }
    
    /// <summary>
    /// Upload a file
    /// </summary>
    [Route("Upload")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadFileResponse>> UploadAsync(IFormFile file)
    {
        return Ok
        (
            new UploadFileResponse
            (
                FileInfoDto.FromModel(await _imageProcessingService.UploadFileAsync(file))
            )
        );
    }

    /// <summary>
    /// Download file
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> DownloadFile(Guid id)
    {
        var fileDto = await _imageProcessingService.GetFileForDownloadAsync(id);

        if (fileDto == null)
        {
            return NotFound();
        }
        
        return File
        (
            fileDto.Content,
            fileDto.Type.MimeType,
            fileDto.OriginalName,
            fileDto.LastModificationTime,
            new EntityTagHeaderValue($"\"{ fileDto.Hash }\"")
        );
    }
}