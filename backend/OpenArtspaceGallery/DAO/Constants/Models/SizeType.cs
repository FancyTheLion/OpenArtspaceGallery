namespace OpenArtspaceGallery.DAO.Constants.Models;

public class SizeType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Type { get; set; }

    public SizeType(Guid id, string Name, int width, int height, int type)
    {
        Id = id;
        Name = Name;
        Width = width;
        Height = height;
        Type = type;
    }
}