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
        Name = name;
        Width = width;
        Height = height;
    }
}