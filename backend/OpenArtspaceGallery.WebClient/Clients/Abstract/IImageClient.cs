using OpenArtspaceGallery.Models.API.Requests.Images;
using OpenArtspaceGallery.Models.API.Responses.Images;

namespace OpenArtspaceGallery.WebClient.Clients.Abstract;

/// <summary>
/// Interface to work with images
/// </summary>
public interface IImageClient
{
    /// <summary>
    /// Add image
    /// </summary>
    public Task<AddImageResponse> AddImageAsync(AddImageRequest request);
}