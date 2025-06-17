using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Common;

namespace Scraper.Application.Features.ScrapingByOder
{
    public class SetOrderHandler
    {
        private readonly IScrapingJob _scrapingJob;
        private readonly IHtmlAgilityProvider _agilityProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SetOrderHandler> _logger;

        public SetOrderHandler(
            IScrapingJob scrapingJob,
            IHtmlAgilityProvider agilityProvider,
            IUnitOfWork unitOfWork,
            ILogger<SetOrderHandler> logger)
        {
            _scrapingJob = scrapingJob;
            _agilityProvider = agilityProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<bool, Error>> Handle(CancellationToken ct)
        {
            ///////////////////////
            var urls = new List<string>
            {
                "https://habr.com/ru/companies/bothub/news/888370/",
                "https://xn----7sbhblcmf354acdnd4bb7bwitd4y.xn--p1ai/index.php/nash-raion/triumf-yunykh-artistov",
                "https://yandex.ru/support/webmaster/ru/open-graph/intro-open-graph.html"
            };
            //////////////////////




            await _scrapingJob.ProcessAsync();

            return true;
        }


    }
}
