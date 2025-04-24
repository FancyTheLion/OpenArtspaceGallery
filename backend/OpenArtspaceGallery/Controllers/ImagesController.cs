using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

public class ImagesController : ControllerBase
{
    private readonly IFilesService _filesService;
    private readonly IResizeService _resizeService;
    private readonly IImagesSizesService _imagesSizesService;
    
    public ImagesController
    (
        IFilesService filesService,
        IResizeService resizeService,
        IImagesSizesService imagesSizesService
    )
    {
        _filesService = filesService;
        _resizeService = resizeService;
        _imagesSizesService = imagesSizesService;
    }

    /// <summary>
    /// Upload a file
    /// </summary>
    [Route("Upload")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<Guid>> Upload(ImageForUploadRequest image, IFormFile file)
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
                image.ImageForUpload.Name, 
                file.ContentType, 
                fileBytes);
            
            return Ok(fileGuid);
        }
    }

    /// <summary>
    /// Add image
    /// </summary>
    [Route("AddImage")]
    [HttpPost]
    public async Task<ActionResult<Guid>> AddImage(AddImageRequest image)
    {
        var file = await _filesService.GetFileForDownloadAsync(image.AddImage.SourceFileId);

        if (file == null)
        {
            return NotFound();
        }

        var imageResult = await _filesService.AddImageAsync
        (
            image.AddImage.Name,
            image.AddImage.Description,
            image.AddImage.AlbumId
        );
        
        var sizes = await _imagesSizesService.GetListAsync();

        var resizedImages = await _resizeService.GenerateImagesSetAsync(image.AddImage.SourceFileId, sizes);
        
        return Ok(imageResult);
    }
}