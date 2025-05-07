namespace OpenArtspaceGallery.Models.ImagesSizes;

public class ImageSizeType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
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