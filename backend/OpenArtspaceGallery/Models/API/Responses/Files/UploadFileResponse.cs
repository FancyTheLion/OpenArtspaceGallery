using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Files;

namespace OpenArtspaceGallery.Models.API.Responses.Files;

public class UploadFileResponse
{
    [JsonPropertyName("fileInfo")]
    public FileInfoDto FileInfo { get; private set; }

    public UploadFileResponse
    (
        FileInfoDto fileInfo
    )
    {
        FileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo), "File info must not be null!");
    }
}