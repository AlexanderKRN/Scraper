using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Scraper.Application.Features;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using Scraper.Infrastructure.DbContexts;

namespace Scraper.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ScraperWriteDbContext _dbContext;

    public OrderRepository(ScraperWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(OrderToScrape order, CancellationToken ct)
    {
        await _dbContext.Orders.AddAsync(order, ct);
    }

    public async Task<Result<OrderToScrape, Error>> GetById(Guid id, CancellationToken ct)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(s => s.Id == id, ct);
        if (order is null)
            return ErrorList.General.NotFound();

        return order;
    }
}
