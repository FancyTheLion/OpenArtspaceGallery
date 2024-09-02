using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
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
}