using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Contexts;

// TODO: Add comment
public class MainDbContext : DbContext
{
    /// <summary>
    /// Albums
    /// </summary>
    public DbSet<AlbumDbo> Albums { get; set; }
    
    /// <summary>
    /// File types
    /// </summary>
    public DbSet<FileTypeDbo> FileTypes { get; set; }
    
    /// <summary>
    /// Image sizes
    /// </summary>
    public DbSet<ImageSizeDbo> ImagesSizes { get; set; }
    
    /// <summary>
    /// Files
    /// </summary>
    public DbSet<FileDbo> Files { get; set; }
    
    /// <summary>
    /// Images
    /// </summary>
    public DbSet<ImageDbo> Images { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<AlbumDbo>()
            .HasMany(a => a.Images)
            .WithOne(i => i.Album);
        
        modelBuilder
            .Entity<FileDbo>()
            .HasOne<FileTypeDbo>(f => f.Type);

        modelBuilder
            .Entity<ImageFileDbo>()
            .HasOne<ImageSizeDbo>(i => i.Size);

        modelBuilder
            .Entity<ImageFileDbo>()
            .HasOne<FileDbo>(i => i.File);

        modelBuilder
            .Entity<ImageDbo>()
            .HasMany<ImageFileDbo>(i => i.Files)
            .WithOne(i => i.Image);
    }
}