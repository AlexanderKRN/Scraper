using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;

namespace Scraper.Application.Features;

/// <summary>
/// Интерфейс репозитория работы с ордерами
/// </summary>
public interface IOrderRepository
{
    Task Add(OrderToScrape order, CancellationToken ct);
    Task<Result<OrderToScrape, Error>> GetById(Guid id, CancellationToken ct);
}