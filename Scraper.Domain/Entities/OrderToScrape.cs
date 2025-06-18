using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.Entities;

public class OrderToScrape
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<string> Urls => _urls;
    private readonly List<string> _urls = [];

    public IReadOnlyList<ScrapingNotice> Notices => _notices;
    private readonly List<ScrapingNotice> _notices = [];

    private OrderToScrape()
    {
    }

    private OrderToScrape(IEnumerable<string> urls)
    {
        _urls = urls.ToList();
        CreatedAt = DateTime.UtcNow;
    }

    public Result<bool, Error> AddNotice(ScrapingNotice notice)
    {
        _notices.Add(notice);
        return true;
    }

    public static Result<OrderToScrape, Error> Create (
        IEnumerable<string> urls)
    {
        var listPathes = urls.ToList();
        if (listPathes.Count >= Constraints.LINKS_COUNT_LIMIT)
            return ErrorList.General.ValueIsInvalid();

        return new OrderToScrape(urls);
    }
}
