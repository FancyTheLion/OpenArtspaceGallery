using OpenArtspaceGallery.Models;

namespace OpenArtspaceGallery.Tests.Helpers;

/// <summary>
/// Helper class for testing ImagesSizes
/// </summary>
public static class ImagesSizesHelper
{
    private const int MinWidth = 30;
    private const int MaxWidth = 100000;
    
    private const int MinHeight = 30;
    private const int MaxHeight = 100000;
    
    private static IEnumerator<ImageSize> _imagesSizesEnumerator = CreateNextImageSize().GetEnumerator();
    
    private static IEnumerable<ImageSize> CreateNextImageSize()
    {
        for (int height = MinHeight; height < MaxHeight; height++)
        {
            for (int width = MinWidth; width < MaxWidth; width++)
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