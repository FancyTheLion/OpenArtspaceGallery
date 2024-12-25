namespace OpenArtspaceGallery.DAO.Models.Images;

public class UpdateImageSizeDbo
{
    /// <summary>
    /// Image size name 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Image size width 
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// Image size height  
    /// </summary>
    public int Height { get; set; }
}