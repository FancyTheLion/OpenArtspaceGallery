using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Shared;

namespace OpenArtspaceGallery.Models.API.Responses.ImagesSizes;

public class ImageSizeNameExistenceResponse
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("existence")]
    public ExistenceDto NameExistence { get; private set; }
    
    public ImageSizeNameExistenceResponse
    (
        ExistenceDto nameExistence
    )
    {
        NameExistence = nameExistence ?? throw new ArgumentNullException(nameof(nameExistence), "Image size name can't be null.");
    }
}