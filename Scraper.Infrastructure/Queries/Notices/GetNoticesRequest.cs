namespace Scraper.Infrastructure.Queries.Notices;

/// <summary>
/// Модель данных в запросе на получение записей по ордеру
/// </summary>
/// <param name="OrderId"> Id ордера </param>
public record GetNoticesRequest(Guid OrderId);