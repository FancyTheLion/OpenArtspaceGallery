using OpenArtspaceGallery.Models.API.DTOs.Files;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class FilesService : IFilesService
{
    public async Task<FileInfoDto> UploadFileAsync(IFormFile file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null!");
        
        var folderPath = "/home/fancy/Projects/OpenArtspaceGalleryStorage";

        var filePath = Path.Combine(folderPath, file.FileName);
        
        var content = new byte[file.Length];

        using (var fileStream = file.OpenReadStream())
        {
            fileStream.Read(content, 0, (int)file.Length);
        }

        await File.WriteAllBytesAsync(filePath, content);
        
        var fileId = Guid.NewGuid();

        return new FileInfoDto(fileId, file.FileName);
    }
}