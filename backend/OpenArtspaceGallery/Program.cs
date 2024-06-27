using OpenArtspaceGallery.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Settings

    builder.Services.Configure<SiteInfoSettings>(builder.Configuration.GetSection(nameof(SiteInfoSettings)));

#endregion

builder.Services.AddControllers(); //регистрирует все, что необходимо для разработки веб-API. Услуги включают поддержку контроллеров, привязку модели, API Explorer, авторизацию, CORS, проверки, сопоставление форматтера и т. д. 

#region CORS

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
                    "http://localhost:5173" // TODO: Move to settings
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

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
