using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.Settings;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/SiteInfo")]
public class SiteInfoController : ControllerBase
{
    private readonly SiteInfoSettings _siteInfoSettings;

    public SiteInfoController
    (
        IOptions<SiteInfoSettings> siteInfoSettings
    )
    {
        _siteInfoSettings = siteInfoSettings.Value;
    }

    /// <summary>
    /// Send backend version to frontend
    /// </summary>
    [HttpGet]
    [Route("GetBackendVersion")]
    public async Task<ActionResult<BackendVersionResponse>> GetBackendVersion()
    {
        return new BackendVersionResponse(new BackendVersionDto("0.0.1"));
    }
    
    /// <summary>
    /// Get link to source code
    /// </summary>
    [Route("GetSourcesLink")]
    [HttpGet]
    public async Task<ActionResult<SourcesLinkResponse>> GeSourcesLink()
    {
        return Ok(new SourcesLinkResponse(new SourcesLinkDto(_siteInfoSettings.SourcesUrl)));
    }
}

