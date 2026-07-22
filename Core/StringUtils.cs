namespace Core;

public static class StringUtils
{
    public static IReadOnlyList<string> WhereNotContains(this IReadOnlyList<string> source, string text)
    {
        return source
            .Where(s => s.NotContains(text))
            .ToList();
    }

    private static bool NotContains(this string source, string text)
    {
        return !source.Contains(text, StringComparison.OrdinalIgnoreCase);
    }
}