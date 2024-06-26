using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.Responses;
using OpenArtspaceGallery.Models.API.SiteInfoSettings;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
public class WebsiteInfoController : ControllerBase
{
    private readonly SiteInfoSettings _siteInfoSettings;

    public WebsiteInfoController
    (
        IOptions<SiteInfoSettings> siteInfoSettings
    )
    {
        _siteInfoSettings = siteInfoSettings.Value;
    }

    [HttpGet]
    [Route("api/GetBackendVersion")]
    public async Task<ActionResult<VersionBackendResponse>> GetBackendVersion()
    {
        return new VersionBackendResponse(new VersionBackendDto("Version 0.0.1"));
    }
    
    /// <summary>
    /// Get link to source code
    /// </summary>
    [Route("api/GetSourcesLink")]
    [HttpGet]
    public async Task<ActionResult<SiteInfoSettingsResponse>> GeSourcesLink()
    {
        return Ok(new SiteInfoSettingsResponse(new SiteInfoSettingsDto(_siteInfoSettings.SourcesUrl)));
    }
}

