namespace OpenArtspaceGallery.Helpers.Validators;

public static class GuidValidator
{
    public static void Validate(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new ArgumentException("GUID must not be empty.", nameof(guid));
        }
    }
}