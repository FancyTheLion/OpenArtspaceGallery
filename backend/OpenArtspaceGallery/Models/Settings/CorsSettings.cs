namespace OpenArtspaceGallery.Models.Settings;

/// <summary>
/// appsettings.json CORS settings
/// </summary>
public class CorsSettings
{
    /// <summary>
    /// Allowed domains
    /// </summary>
    public List<string> AllowedDomains { get; set; }
}