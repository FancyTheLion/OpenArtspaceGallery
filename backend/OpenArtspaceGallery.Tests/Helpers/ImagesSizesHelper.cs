using Microsoft.Extensions.Configuration;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.Settings;

namespace OpenArtspaceGallery.Tests.Helpers;

/// <summary>
/// Helper class for testing ImagesSizes
/// </summary>
public class ImagesSizesHelper
{
    private readonly IConfiguration _configuration;
    private readonly ImagesSizesSettings _imagesSizesSettings;
    private readonly IEnumerator<ImageSize> _imagesSizesEnumerator;

    public ImagesSizesHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _imagesSizesSettings = _configuration.GetSection(nameof(ImagesSizesSettings)).Get<ImagesSizesSettings>();
        _imagesSizesEnumerator = CreateNextImageSize().GetEnumerator();
    }
    
    private IEnumerable<ImageSize> CreateNextImageSize()
    {
        for (int height = _imagesSizesSettings.MinHeight; height < _imagesSizesSettings.MaxHeight; height++)
        {
            for (int width = _imagesSizesSettings.MinWidth; width < _imagesSizesSettings.MaxWidth; width++)
            {
                yield return new ImageSize(Guid.NewGuid(), $"Image Size { Guid.NewGuid() }", width, height);
            }
        }

        throw new InvalidOperationException("Too many Images Sizes are used, fix up your tests!");
    }
    
    /// <summary>
    /// Call this method to get next valid Image Size
    /// </summary>
    public ImageSize GetNextImageSize()
    {
        _imagesSizesEnumerator.MoveNext();
        return _imagesSizesEnumerator.Current;
    }
}