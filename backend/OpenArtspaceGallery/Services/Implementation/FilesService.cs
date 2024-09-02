using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Services.Abstract;

namespace OpenArtspaceGallery.Services.Implementation;

public class FilesService : IFilesService
{
    public async Task<IReadOnlyCollection<ImageSize>> GetImagesSizesAsync()
    {
        var imageSizeList = new List<ImageSize>
        {
            new ImageSize(Guid.NewGuid(), "Thumbnail", 150, 100),
            new ImageSize(Guid.NewGuid(), "Small", 480, 320),
        };

        return imageSizeList;
    }
}