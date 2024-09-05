using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs;

/// <summary>
/// Dto for accepting data from the frontend
/// </summary>
public class AddImageSizeDto
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

    public AddImageSizeDto
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
}