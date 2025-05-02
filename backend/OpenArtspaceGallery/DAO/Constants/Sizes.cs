using Type = OpenArtspaceGallery.DAO.Enums.Type;

namespace OpenArtspaceGallery.DAO.Constants;

public static class Sizes
{
    #region Thumbnail
    
    /// <summary>
    /// Size id
    /// </summary>
    public static readonly Guid ThumbnailId = new Guid("ab2f7586-ef5f-4c54-b6e4-868fca797372");
    
    /// <summary>
    /// Size name
    /// </summary>
    public const string ThumbnailName = "Thumbnail";
    
    /// <summary>
    /// Width
    /// </summary>
    public const int ThumbnailWidth = 200; 
    
    /// <summary>
    /// Height
    /// </summary>
    public const int ThumbnailHeight = 150; 
    
    /// <summary>
    /// Type flag to add to the database
    /// </summary>
    public static readonly int ThumbnailType = (int)Type.Preview;
    
    #endregion
    

}