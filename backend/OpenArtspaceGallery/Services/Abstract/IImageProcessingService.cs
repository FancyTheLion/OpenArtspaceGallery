using OpenArtspaceGallery.Models.Albums;
using OpenArtspaceGallery.Models.API.DTOs.Images;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Images;
using OpenArtspaceGallery.Models.ImagesSizes;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Services.Abstract;

public interface IImageProcessingService
{
    /// <summary>
    /// Get image
    /// </summary>
    public Task<Image> GetImageByIdAsync(Guid imageId);
    
    /// <summary>
    /// Add image 
    /// </summary>
    public Task<Image> AddImageAsync(Image image, Guid sourceFileId);

    /// <summary>
    /// Method for mapping
    /// </summary>
    public Task<ImageInfoDto> ToDto(Image image, List<(FileInfo file, ImageSize size)> imageFiles);

}