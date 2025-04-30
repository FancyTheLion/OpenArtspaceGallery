using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Requests.Images;

/// <summary>
/// Wrapper for image creation. Contains image data (title, description, album) and the GUID of
/// the uploaded file. Used in AddImage to create a record in the Images table and generate sizes.
/// </summary>
public class AddImageRequest
{
    /// <summary>
    /// Add image
    /// </summary>
    [JsonPropertyName("image")]
    public AddImageDto Image { get; set; }
}