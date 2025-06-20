namespace Scraper.Domain.Common;

/// <summary>
/// Расширения класса String
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Расширение функциональности метода обработки пустой строки
    /// </summary>
    /// <param name="stringExtended"></param>
    /// <returns></returns>
    public static bool IsEmpty(this string? stringExtended)
    {
        return string.IsNullOrWhiteSpace(stringExtended);
    }
}
