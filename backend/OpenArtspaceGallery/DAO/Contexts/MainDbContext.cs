using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Models.Albums;
using OpenArtspaceGallery.DAO.Models.Files;
using OpenArtspaceGallery.DAO.Models.FilesTypes;
using OpenArtspaceGallery.DAO.Models.Images;

namespace OpenArtspaceGallery.DAO.Contexts;

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
    ///
    /// </summary>
    public DbSet<ImageSizeDbo> ImagesSizes { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<FileDbo> Files { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<ImageDbo> Images { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<ImageFileDbo> ImagesFiles { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Variants are parts of sections
        modelBuilder
            .Entity<FileDbo>()
            .HasOne<FileTypeDbo>() // File имеет один связанный тип файла
            .WithMany(); // Указывает что тип файла может быть связан с множеством файлов, но в то же время связанных файлов может не быть. 
        
        modelBuilder.Entity<FileTypeDbo>()
            .HasMany<FileDbo>() // Обратное отношение, тип файла может иметь несколько связанных файлов
            .WithOne(file => file.Type); // Каждый файл связан с одним типом данных 
        
        modelBuilder.Entity<ImageDbo>()
            .HasMany<ImageFileDbo>() // У одного изображения может быть множество размеров(связь с ImageFileDbo)
            .WithOne(imageFile => imageFile.Image); // Каждая запись связана только с одним изображением
        
        modelBuilder.Entity<ImageSizeDbo>()
            .HasMany<ImageFileDbo>() // Один размер изображения связан с множеством записей в связующей таблице 
            .WithOne(imageFile => imageFile.Size); // Каждая запись связана с одним размером изображения
        
        modelBuilder.Entity<FileDbo>()
            .HasMany<ImageFileDbo>() // Один файл связан с несколькими записями (для разных изображений\размеров)
            .WithOne(imageFile => imageFile.File); // Каждая запись в связующей таблице связана с одним файлом
    }
}