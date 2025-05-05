using System.Text.Json.Serialization;
using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.Helpers.Validators;
using OpenArtspaceGallery.Models.Albums;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeBaseDto
{
    /// <summary>
    /// Size name (large, medium and others)
    /// </summary>
    [JsonPropertyName("name")]
    public String Name { get; private set; }

    /// <summary>
    ///  Image size (Width)
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; private set; }

    /// <summary>
    /// Image size (Height)
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; private set; }
    
    /// <summary>
    /// Is preview?
    /// </summary>
    [JsonPropertyName("type")]
    public ImagesSizesTypes Type { get; set; }

    public ImageSizeBaseDto
    (
        string name,
        int width,
        int height,
        ImagesSizesTypes type
    )
    {
        ImageSizeValidator.Validate(name, width, height);
        
        Name = name;
        Width = width;
        Height = height;
        Type = type;
    }
    
    public virtual ImageSize ToModel()
    {
        return new ImageSize(Guid.Empty, Name, Width, Height, 0);
    }
}