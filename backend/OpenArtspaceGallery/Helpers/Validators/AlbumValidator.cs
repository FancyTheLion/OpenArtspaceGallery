namespace OpenArtspaceGallery.Helpers.Validators;

public static class AlbumValidator
{
    public static void Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name mustn't be null or whitespace.", nameof(name));
        }
    }
}