using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;

namespace Scraper.Application.Features;

public interface IOrderRepository
{
    Task Add(OrderToScrape order, CancellationToken ct);
    Task<Result<OrderToScrape, Error>> GetById(Guid id, CancellationToken ct);
}