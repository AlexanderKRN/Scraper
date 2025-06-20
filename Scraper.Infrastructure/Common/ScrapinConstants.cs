namespace Scraper.Infrastructure.Common;

/// <summary>
/// Постоянные величины
/// </summary>
public static class ScrapinConstants
{
    /// <summary>
    /// XPath путь к метаданным тега "title"
    /// </summary>
    public const string TITLE_XPATH = "/html/head/title";

    /// <summary>
    /// XPath путь к метаданным тега "meta"
    /// </summary>
    public const string META_XPATH = "/html/head/meta";

    /// <summary>
    /// Id ордера для сервиса сбора данных по расписанию
    /// </summary>
    public const string ORDER_ID = "0197882d-eeaa-7847-9b0e-bbadd65b0c67";
}
