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

    public async Task<IReadOnlyCollection<AlbumDbo>> GetAlbumsHierarchyAsync(Guid albumId)
    {
        var result = new List<AlbumDbo>();

        AlbumDbo album = await _dbContext
            .Albums
            .Include(a => a.Parent)
            .SingleAsync(a => a.Id == albumId);
        
        while (album.Parent?.Id != null)
        {
            album = await _dbContext
                .Albums
                .Include(a => a.Parent)
                .SingleAsync(a => a.Id == album.Parent.Id);
            
            result.Add(album);
        }

        result.Reverse();
        
        return result;
    }
}