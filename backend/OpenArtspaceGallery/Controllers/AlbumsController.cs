using System.Xml.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Requests;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
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
    [Route("api/Albums/TopLevel")]
    public async Task<ActionResult<AlbumsListResponse>> GetTopLevelAlbumsListAsync()
    {
        return  new AlbumsListResponse((await _albumsService.GetChildrenAsync(null))
            .Select(a => a.ToDto())
            .ToList());
    }
    
    /// <summary>
    /// Get album list (children)
    /// </summary>
    [HttpGet]
    [Route("api/Albums/ChildrenOf/{albumId}")]
    public async Task<ActionResult<AlbumsListResponse>> GetChildrenAlbumsListAsync(Guid albumId)
    {
        if (!await _albumsService.IsAlbumExistsAsync(albumId))    
        {
            return NotFound();
        }
        
        return  new AlbumsListResponse((await _albumsService.GetChildrenAsync(albumId))
            .Select(a => a.ToDto())
            .ToList());
    }

    [HttpGet]
    [Route("api/Albums/Hierarchy/{albumId}")]
    public async Task<ActionResult<AlbumHierarchyResponse>> GetListAlbumsInHierarchy(Guid albumId)
    {
        if (!await _albumsService.IsAlbumExistsAsync(albumId))
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

    [HttpPost]
    [Route("api/Albums/New")]
    public async Task<ActionResult<NewAlbumResponse>> AddAlbumAsync(NewAlbumRequest request)
    {
        if (request == null)
        {
            return BadRequest("Add album request mustn't be null!");
        }

        if (request.AlbumToAdd == null)
        {
            return BadRequest("when adding an album, information about the album must not be null.");
        }
        
        return new NewAlbumResponse
        (
            (await _albumsService.CreateNewAlbumAsync(request.AlbumToAdd.ToModel())).ToDto()
        );
    }

    [HttpDelete]
    [Route("api/Albums/{albumId}")]
    public async Task<ActionResult> DeleteAlbumAsync(Guid albumId)
    {
        if (!await _albumsService.IsAlbumExistsAsync(albumId))
        {
            return NotFound();
        }

        await _albumsService.DeleteAlbumAsync(albumId);
        
        return Ok();
    }

    [HttpPost]
    [Route("api/Albums/{albumId}/Rename")]
    public async Task<ActionResult> RenameAlbumAsync(Guid albumId, RenameAlbumRequest request)
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
            await _albumsService.RenameAlbumAsync(albumId, request.RenameAlbumInfo.NewName);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        
        return Ok();
    }
}