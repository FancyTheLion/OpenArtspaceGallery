using Microsoft.EntityFrameworkCore;
using OpenArtspaceGallery.DAO.Abstract;
using OpenArtspaceGallery.DAO.Contexts;
using OpenArtspaceGallery.DAO.Implementation;
using OpenArtspaceGallery.Models.Settings;
using OpenArtspaceGallery.Services.Abstract;
using OpenArtspaceGallery.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// DI
#region DI
    #region Scoped
                
    builder.Services.AddScoped<IAlbumsService, AlbumsService>();
    builder.Services.AddScoped<IAlbumsDao, AlbumsDao>();
    builder.Services.AddScoped<IImagesSizesService, ImagesSizesService>();
    builder.Services.AddScoped<IImagesSizesDao, ImagesSizesDao>();
    builder.Services.AddScoped<IFilesService, FilesService>();
    builder.Services.AddScoped<IFilesDao, FilesDao>();

    #endregion
            
#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Settings

    builder.Services.Configure<SiteInfoSettings>(builder.Configuration.GetSection(nameof(SiteInfoSettings)));
    builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection(nameof(CorsSettings)));
    builder.Services.Configure<AlbumsSettings>(builder.Configuration.GetSection(nameof(AlbumsSettings)));
    builder.Services.Configure<ImagesSizesSettings>(builder.Configuration.GetSection(nameof(ImagesSizesSettings)));

#endregion

builder.Services.AddControllers(); 

#region CORS

var corsSettings = builder
                       .Configuration
                       .GetSection(nameof(CorsSettings))
                       .Get<CorsSettings>()
                   ?? throw new ArgumentException("Please set up CORS settings in appsettings.json");

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy
    (
        policy =>
        {
            policy
                .WithOrigins
                (
                    corsSettings.AllowedDomains.ToArray()
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

#endregion

#region  DB Contexts

if (builder.Environment.IsEnvironment("Testing"))
{
    // Test mode
    #region Main
    
    builder.Services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
    
    #endregion
}
else
{
    // Normal mode
    
    #region Main
    
    builder.Services.AddDbContext<MainDbContext>
    (
        options
            =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("MainConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)),
        ServiceLifetime.Transient
    );
    
    #endregion
}

#endregion

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
        
app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }