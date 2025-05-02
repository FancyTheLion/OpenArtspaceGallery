using System.Text.Json.Serialization;
using OpenArtspaceGallery.Helpers.Validators;
using OpenArtspaceGallery.Models.Albums;
using Type = OpenArtspaceGallery.DAO.Enums.Type;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeDto : ImageSizeBaseDto
{
    /// <summary>
    /// Image size id
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }
    
    public ImageSizeDto
    (
        Guid id,
        string name,
        int width,
        int height,
        Type type
    ) : base(name, width, height, type)
    {
        Id = id;
    }
    
    public override ImageSize ToModel()
    {
        return new ImageSize(Id, Name, Width, Height, 0);
    }
}