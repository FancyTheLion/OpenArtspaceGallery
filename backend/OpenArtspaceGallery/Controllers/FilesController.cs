using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.Models.API.Responses.Files;
using OpenArtspaceGallery.Models.Files;
using OpenArtspaceGallery.Models.Settings;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Controllers;

[ApiController]
[Route("api/Files")]
public class FilesController : ControllerBase
{
    private readonly IFilesService _filesService;
    private readonly IFilesDao _filesDao;
    private readonly MainDbContext _dbContext;
    private readonly FilesStorageSettings _filesStorageSettings;

    public FilesController
    (
        IFilesService filesService,
        IFilesDao filesDao,
        MainDbContext dbContext,
        IOptions<FilesStorageSettings> filesStorageSettings
    )
    {
        _filesService = filesService;
        _filesStorageSettings = filesStorageSettings.Value;
        _filesDao = filesDao;
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Upload a file
    /// </summary>
    [Route("Upload")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadFileResponse>> UploadAsync(IFormFile file)
    {
        return Ok(new UploadFileResponse(await _filesService.UploadFileAsync(file)));
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var fileEntity = await _dbContext.Files
            .Include(f => f.Type) 
            .FirstOrDefaultAsync(f => f.Id == id);

        if (fileEntity == null)
            return NotFound();
        
        var absolutePath = Path.Combine(_filesStorageSettings.RootPath, fileEntity.StoragePath);
        if (!System.IO.File.Exists(absolutePath))
            return NotFound("Файл на диске не найден");
        
        var fileBytes = await System.IO.File.ReadAllBytesAsync(absolutePath);
        
        var contentType = GetMimeType(fileEntity.OriginalName);
        return File(fileBytes, contentType, fileEntity.OriginalName);
    }
    
    private string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(fileName, out var contentType))
        {
            contentType = "application/octet-stream"; 
        }

        return contentType;
    }
}