using Microsoft.Extensions.Logging;
using Scraper.Domain.Entities;
using Scraper.Application.Providers;

namespace Scraper.Infrastructure.Jobs;

public class ScrapingJob : IScrapingJob
{
    private readonly IHtmlAgilityProvider _htmlAgility;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ScrapingJob> _logger;

    public ScrapingJob(
        IHtmlAgilityProvider htmlAgility,
        IUnitOfWork unitOfWork,
        ILogger<ScrapingJob> logger)
    {
        _htmlAgility = htmlAgility;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task ProcessAsync()
    {
        var ct = new CancellationTokenSource().Token;

        _logger.LogInformation("Старт сбора данных по заданию");


        var urls = new List<string>
            {
                "https://habr.com/ru/companies/bothub/news/888370/",
                "https://xn----7sbhblcmf354acdnd4bb7bwitd4y.xn--p1ai/index.php/nash-raion/triumf-yunykh-artistov",
                "https://yandex.ru/support/webmaster/ru/open-graph/intro-open-graph.html"
            };

        List<ScrapingNotice> notices = [];

        foreach (var url in urls)
        {
            var notice = await _htmlAgility.GetDataByUrl(url, ct);

            if (notice.IsFailure)
            {
                var faultNotice = ScrapingNotice.Create(url, null);

                faultNotice.Value.ErrorScraping = notice.Error.Message;

                notices.Add(faultNotice.Value);
            }

            if (notice.IsSuccess)
                notices.Add(notice.Value);
        }


        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation("Завершение сбора данных по заданию");

    }
}
