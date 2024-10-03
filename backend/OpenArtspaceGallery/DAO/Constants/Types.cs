namespace OpenArtspaceGallery.DAO.Constants;

// TODO: Fix English here
// TODO: static readonly and const
// TODO: Fix naming
// TODO: Add comment
public static class Types
{
    // TODO: JPEG is abbreviation
    #region Jpeg
    
    /// <summary>
    /// Type id (jpeg)
    /// </summary>
    public static readonly Guid TypeIdJpeg = new Guid("357ded90-5274-4a5b-9161-ca357a1c1eea");
    
    /// <summary>
    /// Image mime type (jpeg)
    /// </summary>
    public const string MimeTypeJpeg = "image/jpeg";
    
    /// <summary>
    /// Description type (jpeg)
    /// </summary>
    public const string DescriptionTypeJpeg = "Uses file extensions: jpeg, .jpg"; // TODO: TypeDescriptionJpeg
    
    #endregion

    #region Png

    /// <summary>
    /// Type id (png)
    /// </summary>
    public static Guid TypeIdPng = new Guid("575335b1-6f77-422f-86cc-ec929fbb685a");
    
    /// <summary>
    /// Image mime type (png)
    /// </summary>
    public static string MimeTypePng = new string("image/png");
    
    /// <summary>
    /// Description type (png)
    /// </summary>
    public static string DescriptionTypePng = new string("Uses file extensions: .png");

    #endregion

    #region Gif

    /// <summary>
    /// Type id (Gif)
    /// </summary>
    public static Guid TypeIdGif = new Guid("73d52adf-22d3-425c-8982-02fc323a862f");
    
    /// <summary>
    /// Image mime type (Gif)
    /// </summary>
    public static string MimeTypeGif = new string("image/gif");
    
    /// <summary>
    /// Description type (Gif)
    /// </summary>
    public static string DescriptionTypeGif = new string("Uses file extensions: .gif");

    #endregion

    #region Bmp

    /// <summary>
    /// Type id (Bmp)
    /// </summary>
    public static Guid TypeIdBmp = new Guid("b7d6f2eb-0ded-473d-9fe0-433565a127e9");
    
    /// <summary>
    /// Image mime type (Bmp)
    /// </summary>
    public static string MimeTypeBmp = new string("image/bmp");
    
    /// <summary>
    /// Description type (Bmp)
    /// </summary>
    public static string DescriptionTypeBmp = new string("Uses file extensions: .bmp");

    #endregion

    #region WebP
    
    /// <summary>
    /// Type id (WebP)
    /// </summary>
    public static Guid TypeIdWebP = new Guid("9060f26e-8544-49af-9c37-a2dad4bd574f");
    
    /// <summary>
    /// Image mime type (WebP)
    /// </summary>
    public static string MimeTypeWebP = new string("image/webp");
    
    /// <summary>
    /// Description type (WebP)
    /// </summary>
    public static string DescriptionTypeWebP = new string("Uses file extensions: .webp");
    
    #endregion
    
    #region Tiff
    
    /// <summary>
    /// Type id (Tiff)
    /// </summary>
    public static Guid TypeIdTiff = new Guid("c7eeba7b-d085-495d-a879-91534d9affb8");
    
    /// <summary>
    /// Image mime type (Tiff)
    /// </summary>
    public static string MimeTypeTiff = new string("image/tiff");
    
    /// <summary>
    /// Description type (Tiff)
    /// </summary>
    public static string DescriptionTypeTiff = new string("Uses file extensions: .tiff, .tif");
    
    #endregion
    
    #region Ico
    
    /// <summary>
    /// Type id (Ico)
    /// </summary>
    public static Guid TypeIdIco = new Guid("94ad1de6-760a-477b-a22d-c45804e55fa2");
    
    /// <summary>
    /// Image mime type (Ico)
    /// </summary>
    public static string MimeTypeIco = new string("image/x-icon");
    
    /// <summary>
    /// Description type (Ico)
    /// </summary>
    public static string DescriptionTypeIco = new string("Uses file extensions: .ico");
    
    #endregion
    
    #region Svg
    
    /// <summary>
    /// Type id (Svg)
    /// </summary>
    public static Guid TypeIdSvg = new Guid("9f1a29ca-d2df-4e1d-9c5c-7ac1be1d74aa");
    
    /// <summary>
    /// Image mime type (Svg)
    /// </summary>
    public static string MimeTypeSvg = new string("image/svg+xml");
    
    /// <summary>
    /// Description type (Svg)
    /// </summary>
    public static string DescriptionTypeSvg = new string("Uses file extensions: .svg");
    
    #endregion
    
    #region Heic
    
    /// <summary>
    /// Type id (Heic)
    /// </summary>
    public static Guid TypeIdHeic = new Guid("de4a82cb-9305-418a-a2c9-4b6182defe35");
    
    /// <summary>
    /// Image mime type (Heic)
    /// </summary>
    public static string MimeTypeHeic = new string("image/heic");
    
    /// <summary>
    /// Description type (Heic)
    /// </summary>
    public static string DescriptionTypeHeic = new string("Uses file extensions: .heic");
    
    #endregion
}