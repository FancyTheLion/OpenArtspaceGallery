using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Constants.ImagesSizes;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Files.Images;
using OpenArtspaceGallery.Models.Images;
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
            .FirstAsync(img => img.Id == imageId);
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
        return await _dbContext.Images
            .Include(x => x.Album)
            .Where(img => img.Album.Id == albumId)
            .ToListAsync();
    }
    
    public async Task<IReadOnlyDictionary<Guid, Guid>>  GetThumbnailsIdsForImagesAsync(IReadOnlyCollection<Guid> imageIds)
    {
        var result = await _dbContext
            .ImagesFiles
            .Include(imgf => imgf.File)
            .Where(imgf => imageIds.Contains(imgf.Image.Id) && imgf.Size.Id == ImagesSizes.Thumbnail.Id)
            .ToDictionaryAsync(imgf => imgf.Image.Id, imgf => imgf.File?.Id ?? Guid.Empty);
        
        return imageIds
            .ToDictionary(imgid => imgid, imgid => result.TryGetValue(imgid, out var value) ? value : Guid.Empty);
    }
    
    public async Task<IReadOnlyCollection<ImageDbo>> GetLastImagesInAlbumAsync(Guid albumId, int count)
    {
        return await _dbContext
            .Images
            .Include(img => img.Album)
            .Where(img => img.Album.Id == albumId)
            .OrderByDescending(img => img.CreationTime)
            .Take(count)
            .ToListAsync();
    }
    
    public async Task<Guid?> GetOriginalIdAsync(Guid imageFileId)
    {
        var imageId = await _dbContext
            .ImagesFiles
            .Include(x => x.Image)
            .Where(x => x.Id == imageFileId)
            .Select(x => x.Image.Id)
            .FirstOrDefaultAsync();
        
        return await _dbContext.ImagesFiles
            .Where(f => f.Image.Id == imageId && f.Size.Type == ImagesSizesTypes.Original)
            .Select(f => (Guid?)f.File.Id) 
            .FirstOrDefaultAsync();
    }
}