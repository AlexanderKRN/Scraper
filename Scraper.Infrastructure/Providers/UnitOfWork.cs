using Scraper.Application.Providers;
using Scraper.Infrastructure.DbContexts;

namespace Scraper.Infrastructure.Providers;

/// <summary>
/// Провайдер UnitOfWork
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly ScraperWriteDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext"> Контекст базы данных </param>
    public UnitOfWork(ScraperWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Сохранение изменений
    /// </summary>
    /// <param name="ct"> Токен отмены </param>
    /// <returns></returns>
    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }
}
