using OpenArtspaceGallery.DAO.Models.Images;
using OpenArtspaceGallery.Helpers.Validators;
using OpenArtspaceGallery.Models.API.DTOs.ImagesSizes;
using Type = OpenArtspaceGallery.DAO.Enums.Type;

namespace OpenArtspaceGallery.Models.Albums;

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
    
    /// <summary>
    /// Image type
    /// </summary>
    public Type Type { get; set; }

    public ImageSize
    (
        Guid id,
        string name,
        int width,
        int height,
        Type type
    )
    {
        ImageSizeValidator.Validate(name, width, height);
        
        Id = id;
        Name = name;
        Width = width;
        Height = height;
        Type = type;
    }

    public static ImageSize? FromDbo
    (
        ImageSizeDbo? imageSize
    )
    {
        if (imageSize == null)
        {
            return null;
        }
        
        ImageSizeValidator.Validate(imageSize.Name, imageSize.Width, imageSize.Height);
        
        return new ImageSize
        (
            imageSize.Id,
            imageSize.Name,
            imageSize.Width,
            imageSize.Height,
            imageSize.Type
        );
    }

    public ImageSizeDto ToDto()
    {
        return new ImageSizeDto
        (
            Id,
            Name,
            Width,
            Height,
            Type
        );
    }
}