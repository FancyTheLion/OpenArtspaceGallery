using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

public class UpdateImageSizeDto
{
    /// <summary>
    /// Image size name 
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }
    
    /// <summary>
    /// Image size width 
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; private set; }
    
    /// <summary>
    /// Image size height  
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; private set; }
    
    public UpdateImageSizeDto
    (
        string name,
        int width,
        int height
    )
    {
        Name = name;
        Width = width;
        Height = height;
    }
    
    public UpdateImageSize ToModel()
    {
        return new UpdateImageSize(Name, Width, Height);
    }
}