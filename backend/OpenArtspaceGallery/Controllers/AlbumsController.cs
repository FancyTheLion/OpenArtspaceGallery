using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

public class AlbumsController
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
    public async Task<ActionResult<AlbumsListResponse>> GetChildrenAlbumsListAsync(Guid? albumId)
    {
        return  new AlbumsListResponse((await _albumsService.GetChildrenAsync(albumId))
            .Select(a => a.ToDto())
            .ToList());
    }
}