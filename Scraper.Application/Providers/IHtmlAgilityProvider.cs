using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Infrastructure.Providers
{
    public interface IHtmlAgilityProvider
    {
        Task<Result<bool, Error>> GetData(CancellationToken ct);
    }
}