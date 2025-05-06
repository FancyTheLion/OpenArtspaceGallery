using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
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

    public async Task<ImageDbo> AddImageAsync(ImageDbo image)
    {
        image.Album = await _dbContext.Albums.SingleAsync(a => a.Id == image.Album.Id); 
        
        await _dbContext
            .Images
            .AddAsync(image);

        await _dbContext.SaveChangesAsync();

        return image;
    }

    /*public async Task<ImageFileDbo> AddImageFileAsync(Guid imageId, Guid fileId, Guid sizeId)
    {
        var image = new ImageDbo { Id = imageId };
        var file = new FileDbo { Id = fileId };
        var size = new ImageSizeDbo { Id = sizeId };

        _dbContext.Attach(image);
        _dbContext.Attach(file);
        _dbContext.Attach(size);

        var imageFile = new ImageFileDbo
        {
            Id = Guid.NewGuid(),
            Image = image,
            File = file,
            Size = size
        };

        await _dbContext.ImagesFiles.AddAsync(imageFile);
        await _dbContext.SaveChangesAsync();

        return imageFile;
    }*/
}