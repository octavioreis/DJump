public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
    }
}
