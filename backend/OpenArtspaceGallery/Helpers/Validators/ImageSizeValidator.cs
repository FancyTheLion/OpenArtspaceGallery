using OpenArtspaceGallery.DAO.Enums;

namespace OpenArtspaceGallery.Helpers.Validators;

/// <summary>
/// Validator for checking image size
/// </summary>
public static class ImageSizeValidator
{
    public static void Validate(string name, int width, int height, ImagesSizesTypes type)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name mustn't be null or whitespace.", nameof(name));
        }
        
        if (width <= 0 && type != ImagesSizesTypes.Original)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero!");
        }
        
        if (height <= 0 && type != ImagesSizesTypes.Original)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero!");
        }
    }
}