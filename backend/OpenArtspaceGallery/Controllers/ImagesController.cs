using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

public class ImagesController : ControllerBase
{
    private readonly IFilesService _filesService;

    public ImagesController
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
    public async Task<ActionResult<Guid>> Upload(string name, string description, Guid albumId, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest();
        }
        
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            
            var fileGuid = await _filesService.SaveFileAsync(
                name, 
                file.ContentType, 
                fileBytes);
            
            return Ok(fileGuid);
        }
    }
}