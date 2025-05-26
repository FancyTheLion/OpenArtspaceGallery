using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Files.Images;
using OpenArtspaceGallery.Models.ImagesSizes;

namespace OpenArtspaceGallery.DAO.Implementation;

public class ImagesDao : IImagesDao
{
    private readonly MainDbContext _dbContext;

    public ImagesDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<ImageDbo> GetImageByIdAsync(Guid imageId)
    {
        return await _dbContext
            .Images
            .Include(img => img.Album)
            .FirstOrDefaultAsync(img => img.Id == imageId);
    }

    public async Task<ImageDbo> AddImageAsync(ImageDbo image)
    {
        image.Album = await _dbContext.Albums.SingleAsync(a => a.Id == image.Album.Id);

        foreach (var file in image.Files)
        {
            file.File = await _dbContext.Files.SingleAsync(f => f.Id == file.File.Id);
            file.Size = await _dbContext.ImagesSizes.SingleAsync(s => s.Id == file.Size.Id);
        }
        
        await _dbContext
            .Images
            .AddAsync(image);

        await _dbContext.SaveChangesAsync();

        return image;
    }

    public async Task<IReadOnlyDictionary<Guid, Guid?>> GetFilesIdsBySizesIdsAsync(Guid id, IReadOnlyCollection<Guid> sizesIds)
    {
        return await _dbContext
            .ImagesFiles
            
            .Include(imgf => imgf.File)
            .Include(imgf => imgf.Size)
                
            .Where(imgf => imgf.Image.Id == id)
            .Where(imgf => sizesIds.Contains(imgf.Size.Id))
            .ToDictionaryAsync(imgf => imgf.Size.Id, imgf => imgf?.File.Id);
    }

    public async Task<IReadOnlyCollection<ImageDbo>> GetImagesByAlbumIdAsync(Guid albumId)
    {
        return await _dbContext.Images.
            Include(x => x.Album)
            .Where(img => img.Album.Id == albumId)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<(Guid imageId, Guid fileId)>> GetThumbnailsForImagesAsync(IEnumerable<Guid> imageIds)
    {
        var thumbnailSizeId = ImagesSizes.Thumbnail.Id;
        
        return await _dbContext.ImagesFiles
            .Where(imgf => imageIds.Contains(imgf.Image.Id) && imgf.Size.Id == thumbnailSizeId)
            .Select(im => new ValueTuple<Guid, Guid>(im.Image.Id, im.File.Id))
            .ToListAsync();
    }
}