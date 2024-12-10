using System.Text.Json.Serialization;

namespace OpenArtspaceGallery.Models.API.DTOs.Shared;

public class ExistenceDto
{
    /// <summary>
    /// Does it exist
    /// </summary>
    [JsonPropertyName("isExist")]
    public bool IsExist { get; private set; }
    
    public ExistenceDto(bool isExist)
    {
        IsExist = isExist;
    }
}