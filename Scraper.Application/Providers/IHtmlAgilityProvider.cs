using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;

namespace Scraper.Application.Providers
{
    public interface IHtmlAgilityProvider
    {
        Task<Result<ScrapingNotice, Error>> GetDataByUrl(string url, CancellationToken ct);
    }
}