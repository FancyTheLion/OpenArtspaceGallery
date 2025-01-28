namespace OpenArtspaceGallery.Helpers.Validators;

public static class AlbumValidator
{
    public static bool Validate(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length < 200;
    }
}