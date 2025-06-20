namespace Scraper.Application.Features.ScrapingByOder;

/// <summary>
/// Модель данных в запросе по новому ордеру
/// </summary>
/// <param name="FilePath"> Путь к файлу </param>
public record SetOrderHandlerRequest(string FilePath);
