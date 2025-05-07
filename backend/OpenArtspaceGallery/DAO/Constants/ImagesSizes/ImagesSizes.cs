using OpenArtspaceGallery.DAO.Constants.Models;
using OpenArtspaceGallery.DAO.Enums;

namespace OpenArtspaceGallery.DAO.Constants.ImagesSizes;

public static class ImagesSizes
{
    /// <summary>
    /// Thumbnail size
    /// </summary>
    public static readonly SizeType Thumbnail = new SizeType
    (
        new Guid("ab2f7586-ef5f-4c54-b6e4-868fca797372"),
        "Thumbnail",
        200,
        150,
        (int)ImagesSizesTypes.Preview
    );
    
    /// <summary>
    /// Original size
    /// </summary>
    public static readonly SizeType Original = new SizeType
    (
        new Guid("e5793e78-6362-43ce-9373-b76913e34b8a"),
        "Original",
        -1,
        -1,
        (int)ImagesSizesTypes.Original
    );
    
}