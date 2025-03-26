using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.Responses.Files;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/Files")]
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
    /// Upload a file
    /// </summary>
    [Route("Upload")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadFileResponse>> UploadAsync(IFormFile file)
    {
        //TODO: For test
        if (file.ContentType != "image/png") 
        {
            return BadRequest("Unsupported file type.");
        } 
        
        return Ok(new UploadFileResponse(await _filesService.UploadFileAsync(file)));
    }
}