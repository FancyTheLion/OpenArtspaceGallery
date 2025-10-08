using System.Text.Json.Serialization;
using OpenArtspaceGallery.Models.API.DTOs.Files;

namespace OpenArtspaceGallery.Models.API.Responses.Files;

public class DownloadFileResponse
{
    [JsonPropertyName("downloadFile")]
    public FileForDownloadDto FileForDownload { get; private set; }
}