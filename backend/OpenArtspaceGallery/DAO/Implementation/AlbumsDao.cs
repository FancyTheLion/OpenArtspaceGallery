using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Models.Albums;

namespace OpenArtspaceGallery.DAO.Implementation;

public class AlbumsDao : IAlbumsDao
{
    private readonly MainDbContext _dbContext;
    
    public AlbumsDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<AlbumDbo>> GetChildrenAsync(Guid? parentAlbumId)
    {
        return await _dbContext
            .Albums
            
            .Include(a => a.Parent)    
                
            .Where(a => a.Parent.Id == parentAlbumId)
            .ToListAsync();
    }
}