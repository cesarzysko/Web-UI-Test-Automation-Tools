namespace Core;

public static class StringUtils
{
    public static bool DoesContainText(string source, string text)
    {
        return source.Contains(text, StringComparison.InvariantCultureIgnoreCase);
    }
}