using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using Scraper.Infrastructure.DbContexts;

namespace Scraper.Infrastructure.Queries.Notices;
public class GetNoticesQuery
{
    private readonly ScraperReadDbContext _dbContext;
    private readonly ILogger<GetNoticesQuery> _logger;

    public GetNoticesQuery(
        ScraperReadDbContext dbContext,
        ILogger<GetNoticesQuery> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<GetNoticesResponse, Error>> Handle(
        GetNoticesRequest request,
        CancellationToken ct)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Notices)
            .FirstOrDefaultAsync(n => n.Id == request.OrderId, ct);
        if (order is null)
            return ErrorList.General.NotFound(request.OrderId);

        CreateFile(order.Notices);

        return new GetNoticesResponse(order.Notices);
    }

    private void CreateFile(IEnumerable<ScrapingNotice> data)
    {
        string path = @"c:\temp\OutData.txt";

        try
        {
            using StreamWriter stream = File.CreateText(path);
            foreach (var item in data)
            {
                var json = JsonConvert.SerializeObject(item);
                stream.WriteLine(json);
                stream.WriteLine("");
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка записи в файл: {0}", e.Message);
        }
    }
}
