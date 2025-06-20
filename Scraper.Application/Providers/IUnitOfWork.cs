namespace Scraper.Application.Providers;

/// <summary>
/// Интерфейс провайдера UnitOfWork
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct);
}