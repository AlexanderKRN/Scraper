using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.Queries.Notices;

/// <summary>
/// Модель данных для ответа
/// </summary>
/// <param name="Notices"> Записи </param>
public record GetNoticesResponse(IEnumerable<ScrapingNotice> Notices);