namespace Scraper.Domain.Common
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string? stringExtended)
        {
            return string.IsNullOrWhiteSpace(stringExtended);
        }
    }
}
