using Microsoft.AspNetCore.Http.HttpResults;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Albums;
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

    public async Task<bool> IsAlbumExistsAsync(Guid albumId)
    {
        return await _albumsDao.IsAlbumExistsAsync(albumId);
    }

    public async Task<IReadOnlyCollection<Album>> GetChildrenAsync(Guid? parentAlbumId)
    {
        return (await _albumsDao.GetChildrenAsync(parentAlbumId))
            .Select(a => a.ToModel())
            .ToList();
    }

    public async Task<IReadOnlyCollection<AlbumInHierarchy>> GetAlbumsHierarchyAsync(Guid albumId)
    {
        return (await _albumsDao.GetAlbumsHierarchyAsync(albumId))
            .Select(ah => ah.ToAlbumInHierarchyModel())
            .ToList();
    }

    public async Task<Album> CreateNewAlbumAsync(NewAlbum newAlbum)
    {
        if (newAlbum == null)
        {
            throw new ArgumentNullException(nameof(newAlbum), "New album data cannot be null!");
        }
        
        var albumToInsert = new AlbumDbo()
        {
            Id = Guid.Empty,
            Name = newAlbum.Name,
            Parent = newAlbum.ParentAlbumId.HasValue ? new AlbumDbo() { Id = newAlbum.ParentAlbumId.Value } : null,
            CreationTime = DateTime.UtcNow
        };

        return (await _albumsDao.CreateNewAlbumAsync(albumToInsert)).ToModel();
    }

    public async Task DeleteAlbumAsync(Guid albumId)
    {
        var albumChildrenIdList = await _albumsDao.GetChildrenAlbumbsGuidsAsync(albumId);

        // TODO: To study - read about N+1 problem
        foreach (var childId in albumChildrenIdList)
        {
            await DeleteAlbumAsync(childId);
        }
        
        await _albumsDao.DeleteAlbumAsync(albumId);
    }

    public async Task RenameAlbumAsync(Guid albumId, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("The new name cannot be empty.", nameof(newName));
        }
        
        await _albumsDao.RenameAlbumAsync(albumId, newName);
    }
}