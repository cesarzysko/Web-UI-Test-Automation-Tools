namespace Core;

public static class FileNameSanitizer
{
    private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();

    public static string Sanitize(string value)
    {
        return string.Concat(value.Select(c => InvalidChars.Contains(c) ? '_' : c));
    }
}