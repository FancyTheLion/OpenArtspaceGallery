using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Requests;

public class AddImageSizeRequest
{
    /// <summary>
    /// New Image Size Entry
    /// </summary>
    [JsonPropertyName("imageSizeNewEntry")]
    public AddImageSizeDto AddImageSize { get; set; }
}