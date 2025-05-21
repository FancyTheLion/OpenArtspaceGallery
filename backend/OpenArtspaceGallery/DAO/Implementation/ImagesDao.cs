using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;

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
}