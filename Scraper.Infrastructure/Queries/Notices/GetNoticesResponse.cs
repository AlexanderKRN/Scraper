using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.Queries.Notices;
public record GetNoticesResponse(IEnumerable<ScrapingNotice> Notices);