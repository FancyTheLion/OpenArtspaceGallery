namespace OpenArtspaceGallery.DAO.Constants.Models;

public class ImageType
{
    public Guid Id { get; }
    public string MimeType { get; }
    public string Description { get; }

    public ImageType(Guid id, string mimeType, string description)
    {
        Id = id;
        MimeType = mimeType;
        Description = description;
    }
}