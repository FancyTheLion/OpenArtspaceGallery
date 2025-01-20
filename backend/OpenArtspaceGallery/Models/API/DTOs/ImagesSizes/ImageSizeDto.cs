using System.Text.Json.Serialization;
using OpenArtspaceGallery.Helpers.Validators;

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
        int height
    ) : base(name, width, height)
    {
        Id = id;
    }
    
    public override ImageSize ToModel()
    {
        return new ImageSize(Id, Name, Width, Height);
    }
}