using OpenArtspaceGallery.Models.API.Requests.Albums;
using OpenArtspaceGallery.Models.API.Responses.Files;

namespace OpenArtspaceGallery.WebClient.Clients.Abstract;

/// <summary>
/// Interface to work with files
/// </summary>
public interface IFilesClient
{
    /// <summary>
    /// Upload a file
    /// </summary>
    Task<UploadFileResponse> UploadAsync(string filename, string mimeType, byte[] content);
}