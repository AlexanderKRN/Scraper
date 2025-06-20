using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Scraper.Application.Features;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using Scraper.Infrastructure.DbContexts;

namespace Scraper.Infrastructure.Repository;

/// <summary>
/// Репозиторий работы с ордерами
/// </summary>
public class OrderRepository : IOrderRepository
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly ScraperWriteDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext"> Контекст базы данных </param>
    public OrderRepository(ScraperWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Создание нового ордера
    /// </summary>
    /// <param name="order"> Модель ордера </param>
    /// <param name="ct"> Токен отмены </param>
    /// <returns></returns>
    public async Task Add(OrderToScrape order, CancellationToken ct)
    {
        await _dbContext.Orders.AddAsync(order, ct);
    }

    /// <summary>
    /// Получение ордера по Id
    /// </summary>
    /// <param name="id"> Уникальный идентификатор </param>
    /// <param name="ct"> Токен отмены </param>
    /// <returns></returns>
    public async Task<Result<OrderToScrape, Error>> GetById(Guid id, CancellationToken ct)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(s => s.Id == id, ct);
        if (order is null)
            return ErrorList.General.NotFound();

        return order;
    }
}
