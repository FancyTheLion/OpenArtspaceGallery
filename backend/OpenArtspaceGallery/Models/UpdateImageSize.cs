using System.Text.Json.Serialization;
using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models;

public class UpdateImageSize
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
    public int Width { get;  private set; }
    
    /// <summary>
    /// Image size height  
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; private set; }
    
    public UpdateImageSize
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

    public static UpdateImageSize FromDbo
    (
        UpdateImageSizeDbo imageSize
    )
    {
        return new UpdateImageSize
        (
            imageSize.Name,
            imageSize.Width,
            imageSize.Height
        );
    }

    public UpdateImageSizeDto ToDto()
    {
        return new UpdateImageSizeDto
        (
            Name,
            Width,
            Height
        );
    }
}