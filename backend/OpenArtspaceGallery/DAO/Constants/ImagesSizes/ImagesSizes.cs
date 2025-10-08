using OpenArtspaceGallery.DAO.Enums;
using OpenArtspaceGallery.Models.ImagesSizes;

namespace OpenArtspaceGallery.DAO.Constants.ImagesSizes;

public static class ImagesSizes
{
    /// <summary>
    /// Thumbnail size
    /// </summary>
    public static readonly ImageSizeType Thumbnail = new ImageSizeType
    (
        new Guid("ab2f7586-ef5f-4c54-b6e4-868fca797372"),
        "Thumbnail",
        200,
        150,
        (int)ImagesSizeType.Preview
    );
    
    /// <summary>
    /// Original size
    /// </summary>
    public static readonly ImageSizeType Original = new ImageSizeType
    (
        new Guid("e5793e78-6362-43ce-9373-b76913e34b8a"),
        "Original",
        0,
        0,
        (int)ImagesSizeType.Original
    );
    
    /// <summary>
    /// Medium size
    /// </summary>
    public static readonly ImageSizeType MediumDefault = new ImageSizeType
    (
        new Guid("62a5c2bb-1fb7-4514-ab3a-0bb6fc808d2c"),
        "MediumDefault",
        1280,
        960,
        (int)ImagesSizeType.MediumDefault
    );
}