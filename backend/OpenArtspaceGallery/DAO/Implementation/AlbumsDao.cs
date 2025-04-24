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
        // TODO: Rewrite me, inoptimal
        var result = new List<AlbumDbo>();

        var album = await _dbContext
            .Albums
            .Include(a => a.Parent)
            .SingleAsync(a => a.Id == albumId);
        
        result.Add(album);
        
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

    public async Task<AlbumDbo?> GetAlbumByIdAsync(Guid albumId)
    {
        return await _dbContext
            .Albums
                
            .Include(a => a.Parent)
                
            .SingleOrDefaultAsync(a => a.Id == albumId);
    }

    public async Task<bool> IsExistsAsync(Guid albumId)
    {
        return await _dbContext
            .Albums
            .AnyAsync(a => a.Id == albumId);
    }

    public async Task<AlbumDbo?> CreateAsync(AlbumDbo? albumToInsert)
    {
        _ = albumToInsert ?? throw new ArgumentNullException(nameof(albumToInsert), "Album can't be null!");
        
        albumToInsert.Parent = albumToInsert.Parent != null ? await _dbContext.Albums.SingleAsync(a => a.Id == albumToInsert.Parent.Id) : null;

        await _dbContext
            .Albums
            .AddAsync(albumToInsert);
        
        await _dbContext.SaveChangesAsync();

        return albumToInsert;
    }

    public async Task DeleteAsync(Guid albumToDelete)
    {
        var album = await _dbContext
            .Albums
            .SingleAsync(f => f.Id == albumToDelete);

        _dbContext.Remove(album);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Guid>> GetChildrenAlbumbsGuidsAsync(Guid albumId)
    {
        return await _dbContext
            .Albums
            .Where(a => a.Parent.Id == albumId)
            .Select(a => a.Id)
            .ToListAsync();
    }

    public async Task RenameAsync(Guid albumId, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("The new name cannot be empty.", nameof(newName));
        }
        
        var album = await _dbContext
            .Albums
            .SingleAsync(n => n.Id == albumId);
        
        album.Name = newName;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<AlbumDbo> AttachAlbumByIdAsync(Guid albumId)
    {
        var album = new AlbumDbo 
            { Id = albumId };
        
        _dbContext
            .Albums
            .Attach(album);
        
        return album;
    }
}