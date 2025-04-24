using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Images;

namespace OpenArtspaceGallery.Models.API.Requests.Images;

/// <summary>
/// Wrapper for creating an image. Contains image data (title, description, album) and the GUID of
/// the uploaded file. Used in AddImage to create a record in the Images table and generate sizes.
/// </summary>
public class AddImageRequest
{
    /// <summary>
    /// Add image
    /// </summary>
    [JsonPropertyName("addImage")]
    public AddImageDto AddImage { get; set; }
}