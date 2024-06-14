using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
public class WebsiteInfoController : ControllerBase
{
    [HttpGet]
    [Route("api/GetVersionBackend")]
    public async Task<ActionResult<VersionBackendResponse>> GetVersionBackend()
    {
        return new VersionBackendResponse(new VersionBackendDto("Version 0.0.1"));
    }
}