using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs.Images;
using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.Models.API.Responses.Images;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/Images")]
public class ImagesController : ControllerBase
{
    private readonly IImagesService _imagesService;
    private readonly IImagesSizesService _imagesSizesService;
    
    public ImagesController
    (
        IImagesService imagesService,
        IImagesSizesService imagesSizesService
    )
    {
        _imagesService = imagesService;
        _imagesSizesService = imagesSizesService;
    }

    /// <summary>
    /// Add image
    /// </summary>
    [Route("Add")]
    [HttpPost]
    public async Task<ActionResult<AddImageResponse>> AddImageAsync(AddImageRequest request)
    {
        return Ok
        (
            new AddImageResponse
            (
                (await _imagesService.AddImageAsync(request.Image.ToImageModel(), request.Image.SourceFileId))
                .ToDto()
            )
        );
    }

    /// <summary>
    /// Get image info
    /// </summary>
    [Route("{id:guid}")]
    [HttpGet]
    public async Task<ActionResult<ImageInfoResponse>> GetImageInfoAsync(Guid id)
    {
        var image = await _imagesService.GetImageByIdAsync(id);
        if (image == null)
        {
            return NotFound();
        }

        var sizesIds = (await _imagesSizesService.GetListAsync())
            .Select(s => s.Id)
            .ToList();

        var imageFilesIds = (await _imagesService.GetFilesIdsBySizesIdsAsync(id, sizesIds))
            .Where(ifid => ifid.Value != null)
            .ToDictionary(ifid => ifid.Key, imgf => imgf.Value.Value);
        
        return Ok(new ImageInfoResponse(new ImageInfoDto(image, imageFilesIds)));
    }

    /// <summary>
    /// Get image with preview
    /// </summary>
    [Route("/ByAlbum/{albumId:guid}")]
    [HttpGet]
    public async Task<ActionResult<ImageWithPreviewResponse>> GetImageWithPreviewAsync(Guid albumId)
    {
        var images = await _imagesService.GetImagesByAlbumIdAsync(albumId);

        var thumbnails = await _imagesService.GetThumbnailsForImagesAsync(images.Select(x => x.Id));

        var imagesWithPreview = images
            .Select(image =>
            {
                thumbnails.TryGetValue(image.Id, out var thumbId);
                return ImageWithPreviewDto.FromModel(image, thumbId);
            })
            .ToList();
        
        return Ok(new ImageWithPreviewResponse(imagesWithPreview));
    }
}