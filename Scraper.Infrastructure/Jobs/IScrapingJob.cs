namespace Scraper.Infrastructure.Jobs;

/// <summary>
/// Интерфейс сервиса сбора дданных
/// </summary>
public interface IScrapingJob
{
    Task ProcessAsync(Guid id);
}