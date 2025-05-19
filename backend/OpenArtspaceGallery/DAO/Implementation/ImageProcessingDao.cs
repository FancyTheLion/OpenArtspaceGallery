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
}