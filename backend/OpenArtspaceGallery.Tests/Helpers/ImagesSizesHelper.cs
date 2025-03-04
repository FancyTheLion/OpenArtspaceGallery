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
    private readonly ImageSizeSettings _imageSizeSettings;
    private readonly IEnumerator<ImageSize> _imagesSizesEnumerator;

    public ImagesSizesHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _imageSizeSettings = _configuration.GetSection("ImageSizeSettings").Get<ImageSizeSettings>();
        _imagesSizesEnumerator = CreateNextImageSize().GetEnumerator();
    }
    
    private IEnumerable<ImageSize> CreateNextImageSize()
    {
        for (int height = _imageSizeSettings.MinHeight; height < _imageSizeSettings.MaxHeight; height++)
        {
            for (int width = _imageSizeSettings.MinWidth; width < _imageSizeSettings.MaxWidth; width++)
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