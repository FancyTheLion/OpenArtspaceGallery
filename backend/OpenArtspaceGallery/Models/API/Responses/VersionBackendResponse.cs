using OpenArtspaceGallery.Models.API.DTOs;

namespace OpenArtspaceGallery.Models.API.Responses;

public class VersionBackendResponse
{
    public VersionBackendDto VersionBack { get; set; } 

    public VersionBackendResponse(VersionBackendDto versionBack)
    {
        VersionBack = versionBack;
    }
}