namespace OpenArtspaceGallery.DAO.Constants;

/// <summary>
/// File types for working with images
/// </summary>
public static class Types
{
    #region JPEG
    
    /// <summary>
    /// Type id (JPEG)
    /// </summary>
    public static readonly Guid TypeIdJpeg = new Guid("357ded90-5274-4a5b-9161-ca357a1c1eea");
    
    /// <summary>
    /// Image mime type (JPEG)
    /// </summary>
    public const string TypeMimeJpeg = "image/jpeg";
    
    /// <summary>
    /// Description type (JPEG)
    /// </summary>
    public const string TypeDescriptionJpeg = "Uses file extensions: jpeg, .jpg"; 
    
    #endregion

    #region PNG

    /// <summary>
    /// Type id (PNG)
    /// </summary>
    public static readonly Guid TypeIdPng = new Guid("575335b1-6f77-422f-86cc-ec929fbb685a");
    
    /// <summary>
    /// Image mime type (PNG)
    /// </summary>
    public const string TypeMimePng = "image/png";
    
    /// <summary>
    /// Description type (PNG)
    /// </summary>
    public const string TypeDescriptionPng = "Uses file extensions: .png";

    #endregion

    #region GIF

    /// <summary>
    /// Type id (GIF)
    /// </summary>
    public static readonly Guid TypeIdGif = new Guid("73d52adf-22d3-425c-8982-02fc323a862f");
    
    /// <summary>
    /// Image mime type (GIF)
    /// </summary>
    public const string TypeMimeGif = "image/gif";
    
    /// <summary>
    /// Description type (GIF)
    /// </summary>
    public const string TypeDescriptionGif = "Uses file extensions: .gif";

    #endregion

    #region BMP

    /// <summary>
    /// Type id (BMP)
    /// </summary>
    public static readonly Guid TypeIdBmp = new Guid("b7d6f2eb-0ded-473d-9fe0-433565a127e9");
    
    /// <summary>
    /// Image mime type (BMP)
    /// </summary>
    public const string TypeMimeBmp = "image/bmp";
    
    /// <summary>
    /// Description type (BMP)
    /// </summary>
    public const string TypeDescriptionBmp = "Uses file extensions: .bmp";

    #endregion

    #region WebP
    
    /// <summary>
    /// Type id (WebP)
    /// </summary>
    public static readonly Guid TypeIdWebP = new Guid("9060f26e-8544-49af-9c37-a2dad4bd574f");
    
    /// <summary>
    /// Image mime type (WebP)
    /// </summary>
    public const string TypeMimeWebP = "image/webp";
    
    /// <summary>
    /// Description type (WebP)
    /// </summary>
    public const string TypeDescriptionWebP = "Uses file extensions: .webp";
    
    #endregion
    
    #region TIFF
    
    /// <summary>
    /// Type id (TIFF)
    /// </summary>
    public static readonly Guid TypeIdTiff = new Guid("c7eeba7b-d085-495d-a879-91534d9affb8");
    
    /// <summary>
    /// Image mime type (TIFF)
    /// </summary>
    public const string TypeMimeTiff = "image/tiff";
    
    /// <summary>
    /// Description type (TIFF)
    /// </summary>
    public const string TypeDescriptionTiff = "Uses file extensions: .tiff, .tif";
    
    #endregion
    
    #region ICO
    
    /// <summary>
    /// Type id (ICO)
    /// </summary>
    public static readonly Guid TypeIdIco = new Guid("94ad1de6-760a-477b-a22d-c45804e55fa2");
    
    /// <summary>
    /// Image mime type (ICO)
    /// </summary>
    public const string TypeMimeIco = "image/x-icon";
    
    /// <summary>
    /// Description type (ICO)
    /// </summary>
    public const string TypeDescriptionIco = "Uses file extensions: .ico";
    
    #endregion
    
    #region SVG
    
    /// <summary>
    /// Type id (SVG)
    /// </summary>
    public static readonly Guid TypeIdSvg = new Guid("9f1a29ca-d2df-4e1d-9c5c-7ac1be1d74aa");
    
    /// <summary>
    /// Image mime type (SVG)
    /// </summary>
    public const string TypeMimeSvg = "image/svg+xml";
    
    /// <summary>
    /// Description type (SVG)
    /// </summary>
    public const string TypeDescriptionSvg = "Uses file extensions: .svg";
    
    #endregion
    
    #region HEIC
    
    /// <summary>
    /// Type id (HEIC)
    /// </summary>
    public static readonly Guid TypeIdHeic = new Guid("de4a82cb-9305-418a-a2c9-4b6182defe35");
    
    /// <summary>
    /// Image mime type (HEIC)
    /// </summary>
    public const string TypeMimeHeic = "image/heic";
    
    /// <summary>
    /// Description type (HEIC)
    /// </summary>
    public const string TypeDescriptionHeic = "Uses file extensions: .heic";
    
    #endregion
}