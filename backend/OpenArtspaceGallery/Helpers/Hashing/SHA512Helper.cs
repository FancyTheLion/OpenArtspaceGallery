using System.Security.Cryptography;

namespace OpenArtspaceGallery.Helpers.Hashing;

public class SHA512Helper
{
    /// <summary>
    /// Calculate SHA512 hash of file contents
    /// </summary>
    public static string CalculateSHA512(byte[] data)
    {
        var calculator = SHA512.Create();
        var resultBytes = calculator.ComputeHash(data);

        return Convert.ToBase64String(resultBytes, Base64FormattingOptions.None);
    }
}