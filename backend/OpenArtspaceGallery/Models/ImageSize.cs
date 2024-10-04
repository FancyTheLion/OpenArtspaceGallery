using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Models.API.DTOs;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;

namespace OpenArtspaceGallery.Models;

public class ImageSize
{
    /// <summary>
    /// Image size id
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Size name (large, medium and others)
    /// </summary>
    public String Name { get; private set; }

    /// <summary>
    ///  Image size (Width)
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Image size (Height)
    /// </summary>
    public int Height { get; private set; }

    public ImageSize
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

    public static ImageSize FromDbo
    (
        ImageSizeDbo imageSize
    )
    {
        return new ImageSize
        (
            // TODO: Add validation
            imageSize.Id,
            imageSize.Name,
            imageSize.Width,
            imageSize.Height
        );
    }

    public ImageSizeDto ToDto()
    {
        return new ImageSizeDto
        (
            Id,
            Name,
            Width,
            Height
        );
    }
}