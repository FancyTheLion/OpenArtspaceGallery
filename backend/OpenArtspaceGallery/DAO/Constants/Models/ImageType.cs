namespace OpenArtspaceGallery.DAO.Constants.Models;

public class ImageType
{
    public Guid Id { get; set; }
    public string MimeType { get; set; }
    public string Description { get; set; }

    public ImageType(Guid id, string mimeType, string description)
    {
        Id = id;
        MimeType = mimeType;
        Description = description;
    }
}