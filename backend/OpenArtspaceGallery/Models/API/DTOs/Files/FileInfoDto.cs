using System.Text.Json.Serialization;
using FileInfo = OpenArtspaceGallery.Models.Files.FileInfo;

namespace OpenArtspaceGallery.Models.API.DTOs.Files;

public class FileInfoDto
{
    /// <summary>
    /// File ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }

    /// <summary>
    /// Filename
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }
    
    public FileInfoDto
    (
        Guid id,
        string name
    )
    {
        Id = id;
        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("File name must be populated!", nameof(name));
        }
        Name = name;
    }

    /// <summary>
    /// Construct DTO from model
    /// </summary>
    public static FileInfoDto FromModel(FileInfo model)
    {
        _ = model ?? throw new ArgumentNullException(nameof(model));
        
        return new FileInfoDto(model.Id, model.Name);
    }
}