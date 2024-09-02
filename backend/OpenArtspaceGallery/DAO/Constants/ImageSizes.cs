using System.ComponentModel.DataAnnotations;

namespace OpenArtspaceGallery.DAO.Constants;

public class ImageSizes
{
    #region Thumbnail

    /// <summary>
    /// Image size id
    /// </summary>
    public static Guid Id = new Guid("8811ed47-3ec6-490a-8074-42bf4838fdb3");

    /// <summary>
    /// Size name (large, medium and others)
    /// </summary>
    public static String Name = new string("Thumbnail");

    /// <summary>
    ///  Image size (Width)
    /// </summary>
    public static int Width = 100;

    /// <summary>
    /// Image size (Height)
    /// </summary>
    public static int Height = 150;

    #endregion
}