using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class ImageSizeDto
{
    /// <summary>
    /// Image size id
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }

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

    public ImageSizeDto
    (
        Guid id,
        string name,
        int width,
        int height
    )
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name), "Name mustn't be null!");
        
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero!");
        }
        
        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Width must be greater than zero!");
        }
        
        Width = width;
        Height = height;
    }
    
    public ImageSize ToModel()
    {
        return new ImageSize(Id, Name, Width, Height);
    }
}