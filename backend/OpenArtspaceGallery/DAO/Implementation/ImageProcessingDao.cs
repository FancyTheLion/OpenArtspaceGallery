using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Implementation;

public class ImageProcessingDao : IImageProcessingDao
{
    private readonly MainDbContext _dbContext;

    public ImageProcessingDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }
    public async Task<ImageSizeDbo?> GetImageSizeOriginalAsync()
    {
        return await _dbContext
            .ImagesSizes
            .SingleAsync(s => s.Type == ImagesSizesTypes.Original);
    }
    
    public async Task<FileDbo?> GetFileMetadataAsync(Guid fileId)
    {
        return await _dbContext.Files
            .Include(f => f.Type)
            .FirstOrDefaultAsync(f => f.Id == fileId);
    }

    public async Task<ImageDbo> AddImageAsync(ImageDbo image)
    {
        image.Album = await _dbContext.Albums.SingleAsync(a => a.Id == image.Album.Id); 
        
        await _dbContext
            .Images
            .AddAsync(image);

        await _dbContext.SaveChangesAsync();

        return image;
    }
}