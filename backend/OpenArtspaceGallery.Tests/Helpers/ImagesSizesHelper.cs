using Microsoft.Extensions.Configuration;
using OpenArtspaceGallery.Models;
using OpenArtspaceGallery.Models.Settings;

namespace OpenArtspaceGallery.Tests.Helpers;

/// <summary>
/// Helper class for testing ImagesSizes
/// </summary>
public static class ImagesSizesHelper
{
    
    private static readonly ImageSizeSettings _imageSizeSettings;

    static ImagesSizesHelper()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        _imageSizeSettings = config.GetSection("ImageSizeSettings").Get<ImageSizeSettings>();
    }

    public static ImageSizeSettings GetSettings() => _imageSizeSettings;
    
    
    private static IEnumerator<ImageSize> _imagesSizesEnumerator = CreateNextImageSize().GetEnumerator();
    
    private static IEnumerable<ImageSize> CreateNextImageSize()
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
    public static ImageSize GetNextImageSize()
    {
        _imagesSizesEnumerator.MoveNext();
        return _imagesSizesEnumerator.Current;
    }
}