using Microsoft.AspNetCore.Http.HttpResults;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.Albums;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class  AlbumsService : IAlbumsService
{
    private readonly IAlbumsDao _albumsDao;

    public AlbumsService
    (
        IAlbumsDao albumsDao
    )
    {
        _albumsDao = albumsDao;
    }

    public async Task<Album?> GetAlbumByIdAsync(Guid id)
    {
        var dbo = await _albumsDao.GetAlbumByIdAsync(id);
        return dbo != null ? Album.FromDbo(dbo) : null;
    }

    public async Task<bool> IsExistsAsync(Guid albumId)
    {
        return await _albumsDao.IsExistsAsync(albumId);
    }

    public async Task<IReadOnlyCollection<Album>> GetChildrenAsync(Guid? parentAlbumId)
    {
        return (await _albumsDao.GetChildrenAsync(parentAlbumId))
            .Select(Album.FromDbo)
            .ToList();
    }

    public async Task<IReadOnlyCollection<AlbumInHierarchy>> GetAlbumsHierarchyAsync(Guid albumId)
    {
        return (await _albumsDao.GetAlbumsHierarchyAsync(albumId))
            .Select(ah => ah.ToAlbumInHierarchyModel())
            .ToList();
    }

    public async Task<Album> CreateAsync(NewAlbum newAlbum)
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

        return Album.FromDbo(await _albumsDao.CreateAsync(albumToInsert));
    }

    public async Task DeleteAsync(Guid albumId)
    {
        var albumChildrenIdList = await _albumsDao.GetChildrenAlbumbsGuidsAsync(albumId);
        
        foreach (var childId in albumChildrenIdList)
        {
            await DeleteAsync(childId);
        }
        
        await _albumsDao.DeleteAsync(albumId);
    }

    public async Task RenameAsync(Guid albumId, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("The new name cannot be empty.", nameof(newName));
        }
        
        await _albumsDao.RenameAsync(albumId, newName);
    }
    
    
}