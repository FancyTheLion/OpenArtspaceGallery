using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Validators;
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
    public string Name { get; private set; }

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
        ImageSizeValidator.Validate(name, width, height);
        
        Id = id;
        Name = name;
        Width = width;
        Height = height;
    }

    public static ImageSize FromDbo
    (
        ImageSizeDbo imageSize
    )
    {
        ImageSizeValidator.Validate(imageSize.Name, imageSize.Width, imageSize.Height);
        
        return new ImageSize
        (
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