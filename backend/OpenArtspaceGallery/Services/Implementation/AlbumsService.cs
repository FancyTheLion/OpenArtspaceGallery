using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class AlbumsService : IAlbumsService
{
    private readonly IAlbumsDao _albumsDao;

    public AlbumsService
    (
        IAlbumsDao albumsDao
    )
    {
        _albumsDao = albumsDao;
    }

    public async Task<IReadOnlyCollection<Album>> GetChildrenAsync(Guid? parentAlbumId)
    {
        return (await _albumsDao.GetChildrenAsync(parentAlbumId))
            .Select(a => a.ToModel())
            .ToList();
    }
}