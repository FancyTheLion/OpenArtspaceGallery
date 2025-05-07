using OpenArtspaceGallery.Models.Files;

namespace OpenArtspaceGallery.DAO.Constants.MimeTypes;

/// <summary>
/// File types for working with images
/// </summary>
public static class MimeTypes
{
    /// <summary>
    /// JPEG
    /// </summary>
    public static readonly FileType Jpeg = new FileType
    (
        new Guid("357ded90-5274-4a5b-9161-ca357a1c1eea"),
        "image/jpeg",
        "JPEG"
    );
    
    /// <summary>
    /// PNG
    /// </summary>
    public static readonly FileType Png = new FileType
    (
        new Guid("575335b1-6f77-422f-86cc-ec929fbb685a"),
        "image/png",
        "PNG"
    );

    /// <summary>
    /// GIF
    /// </summary>
    public static readonly FileType Gif = new FileType
    (
        new Guid("73d52adf-22d3-425c-8982-02fc323a862f"),
        "image/gif",
        "GIF"
    );
    
    /// <summary>
    /// BMP
    /// </summary>
    public static readonly FileType Bmp = new FileType
    (
        new Guid("b7d6f2eb-0ded-473d-9fe0-433565a127e9"),
        "image/bmp",
        "BMP"
    );
    
    /// <summary>
    /// WebP
    /// </summary>
    public static readonly FileType WebP = new FileType
    (
        new Guid("9060f26e-8544-49af-9c37-a2dad4bd574f"),
        "image/webp",
        "WebP"
    );
    
    /// <summary>
    /// TIFF
    /// </summary>
    public static readonly FileType Tiff = new FileType
    (
        new Guid("c7eeba7b-d085-495d-a879-91534d9affb8"),
        "image/tiff",
        "TIFF"
    );
    
    /// <summary>
    /// ICO
    /// </summary>
    public static readonly FileType Ico = new FileType
    (
        new Guid("94ad1de6-760a-477b-a22d-c45804e55fa2"),
        "image/x-icon",
        "ICO"
    );
    
    /// <summary>
    /// SVG
    /// </summary>
    public static readonly FileType Svg = new FileType
    (
        new Guid("9f1a29ca-d2df-4e1d-9c5c-7ac1be1d74aa"),
        "image/svg+xml",
        "SVG"
    );
    
    /// <summary>
    /// HEIC
    /// </summary>
    public static readonly FileType Heic = new FileType
    (
        new Guid("de4a82cb-9305-418a-a2c9-4b6182defe35"),
        "image/heic",
        "HEIC"
    );
}