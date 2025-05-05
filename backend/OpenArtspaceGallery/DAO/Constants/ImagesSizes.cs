using OpenArtspaceGallery.DAO.Enums;

namespace OpenArtspaceGallery.DAO.Constants;

public static class ImagesSizes
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
    public static readonly int ThumbnailType = (int)ImagesSizesTypes.Preview;
    
    #endregion
    
    #region Original
    
    /// <summary>
    /// Size id
    /// </summary>
    public static readonly Guid OriginalId = new Guid("e5793e78-6362-43ce-9373-b76913e34b8a");
    
    /// <summary>
    /// Size name
    /// </summary>
    public const string OriginalName = "Original";
    
    /// <summary>
    /// Width
    /// </summary>
    public const int OriginalWidth = -1; 
    
    /// <summary>
    /// Height
    /// </summary>
    public const int OriginalHeight = -1; 
    
    /// <summary>
    /// Type flag to add to the database
    /// </summary>
    public static readonly int OriginalType = (int)ImagesSizesTypes.Original;
    
    #endregion
}