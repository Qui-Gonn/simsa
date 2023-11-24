namespace Simsa.Blazor.Shared.Extensions;

public static class StringExtensions
{
    public static string? FirstCharToLowerCase(this string? str)
        => !string.IsNullOrWhiteSpace(str) && char.IsUpper(str[0])
            ? str.Length == 1
                ? char.ToLower(str[0]).ToString()
                : char.ToLower(str[0]) + str[1..]
            : str;
}