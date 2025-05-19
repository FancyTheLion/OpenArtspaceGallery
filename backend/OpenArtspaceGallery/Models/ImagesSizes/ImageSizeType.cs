namespace OpenArtspaceGallery.Models.ImagesSizes;

public class ImageSizeType
{
    /// <summary>
    /// Size type id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Size type name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Width
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// Height
    /// </summary>
    public int Height { get; set; }
    
    /// <summary>
    /// Sizee type
    /// </summary>
    public int Type { get; set; }

    public ImageSizeType(Guid id, string name, int width, int height, int type)
    {
        Id = id;
        Name = name;
        Width = width;
        Height = height;
        Type = type;
    }
}