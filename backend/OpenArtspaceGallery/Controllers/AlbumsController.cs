using System.Xml.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.Shared;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.Responses.Albums;
using OpenArtspaceGallery.Models.API.Responses.Shared;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/Albums")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumsService _albumsService;
        
    public AlbumsController
    (
        IAlbumsService albumsService    
    )
    {
        _albumsService = albumsService;
    }

    /// <summary>
    /// Get album list (top level)
    /// </summary>
    [HttpGet]
    [Route("TopLevel")]
    public async Task<ActionResult<AlbumsListResponse>> GetTopLevelListAsync()
    {
        var albums = (await _albumsService.GetChildrenAsync(null))
            .Select(a => a.ToDto())
            .ToList();
        
            return Ok(new AlbumsListResponse(albums));
    }
    
    /// <summary>
    /// Get album list (children)
    /// </summary>
    [HttpGet]
    [Route("ChildrenOf/{albumId:guid}")]
    public async Task<ActionResult<AlbumsListResponse>> GetChildrenListAsync(Guid albumId)
    {
        if (!await _albumsService.IsExistsAsync(albumId))    
        {
            return NotFound();
        }
        
        return  new AlbumsListResponse
        (
            (await _albumsService.GetChildrenAsync(albumId))
            .Select(a => a.ToDto())
            .ToList()
        );
    }

    /// <summary>
    /// Get album hierarchy
    /// </summary>
    [HttpGet]
    [Route("Hierarchy/{albumId:guid}")]
    public async Task<ActionResult<AlbumHierarchyResponse>> GetListInHierarchyAsync(Guid albumId)
    {
        if (!await _albumsService.IsExistsAsync(albumId))
        {
            return NotFound();
        }
        
        return new AlbumHierarchyResponse
        (
            (await _albumsService.GetAlbumsHierarchyAsync(albumId))
            .Select(ah => ah.ToDto())
            .ToList()
        );
    }

    /// <summary>
    /// Get album info
    /// </summary>
    [HttpGet]
    [Route("{albumId:guid}")]
    public async Task<ActionResult<AlbumInfoResponse>> GetInfoAsync(Guid albumId)
    {
        var album = await _albumsService.GetAlbumByIdAsync(albumId);

        if (album == null)
        {
            return NotFound();
        }
        
        return new AlbumInfoResponse(album.ToDto());
    }

    /// <summary>
    /// Add album
    /// </summary>
    [HttpPost]
    [Route("New")]
    public async Task<ActionResult<NewAlbumResponse>> AddAsync(NewAlbumRequest request)
    {
        if (request == null)
        {
            return BadRequest("Add album request mustn't be null.");
        }

        if (request.AlbumToAdd == null)
        {
            return BadRequest("When adding an album, information about the album must not be null.");
        }

        if (!request.AlbumToAdd.Validate())
        {
            return BadRequest("Invalid new album data was provided.");
        }
        
        return new NewAlbumResponse
        (
            (await _albumsService.CreateAsync(request.AlbumToAdd.ToModel())).ToDto()
        );
    }

    /// <summary>
    /// Delete album
    /// </summary>
    [HttpDelete]
    [Route("{albumId:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid albumId)
    {
        if (!await _albumsService.IsExistsAsync(albumId))
        {
            return NotFound();
        }

        await _albumsService.DeleteAsync(albumId);
        
        return Ok();
    }

    /// <summary>
    /// Rename album
    /// </summary>
    [HttpPost]
    [Route("{albumId:guid}/Rename")]
    public async Task<ActionResult> RenameAsync(Guid albumId, RenameAlbumRequest request)
    {
        if (request == null)
        {
            return BadRequest("Rename album request mustn't be null!");
        }

        if (request.RenameAlbumInfo == null)
        {
            return BadRequest("Rename album name must not be null.");
        }

        try
        {
            await _albumsService.RenameAsync(albumId, request.RenameAlbumInfo.NewName);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
    
    /// <summary>
    /// Is album exists?
    /// </summary>
    [HttpGet]
    [Route("{albumId:guid}/IsExists")]
    public async Task<ActionResult<ExistenceResponse>> IsExists(Guid albumId)
    {
        return new ExistenceResponse(new ExistenceDto(await _albumsService.IsExistsAsync(albumId)));
    }
}