using Microsoft.AspNetCore.Http.HttpResults;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class ImagesSizesService : IImagesSizesService
{
    private readonly IImagesSizesDao _imagesSizesDao;

    public ImagesSizesService
    (
        IImagesSizesDao imagesSizesDao
    )
    {
        _imagesSizesDao = imagesSizesDao;
    }

    public async Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync()
    {
        return (await _imagesSizesDao.GetImagesSizesAsync())
            .Select(i => ImageSize.FromDbo(i))
            .ToList();
    }

    public async Task<ImageSize> AddImageSizeAsync(ImageSize imageSize)
    {
        var newImageSize = new ImageSizeDbo()
        {
            Id = Guid.Empty,
            Name = imageSize.Name,
            Width = imageSize.Width,
            Height = imageSize.Height
        };
        
        if (!await _imagesSizesDao.CheckDuplicatesImageSizeAsync(newImageSize))
        {
            throw new ArgumentException("Image size with this name already exists.", nameof(imageSize.Name));
        }
        
        return ImageSize.FromDbo(await _imagesSizesDao.AddImageSizeAsync(newImageSize));
    }
}