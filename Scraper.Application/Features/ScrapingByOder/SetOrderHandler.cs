using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using System;

namespace Scraper.Application.Features.ScrapingByOder
{
    public class SetOrderHandler
    {
        private readonly IScrapingJob _scrapingJob;
        private readonly IHtmlAgilityProvider _agilityProvider;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SetOrderHandler> _logger;

        public SetOrderHandler(
            IScrapingJob scrapingJob,
            IHtmlAgilityProvider agilityProvider,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            ILogger<SetOrderHandler> logger)
        {
            _scrapingJob = scrapingJob;
            _agilityProvider = agilityProvider;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<bool, Error>> Handle(CancellationToken ct)
        {
            ///////////////////////
            var urls = new List<string>
            {
                "https://nauka.tass.ru/nauka/23318267",
                "https://yandex.ru/support/webmaster/open-graph/intro-open-graph.html",
                "https://www.nytimes.com/2025/03/04/world/asia/china-economy-congress.html",
                "https://xn--80apydf.xn--p1ai/news/za-oknom/istrintsy-zhaluyutsya-na-rabotu-122/",
                "https://xn----7sbhblcmf354acdnd4bb7bwitd4y.xn--p1ai/index.php/nash-raion/triumf-yunykh-artistov",
                "https://habr.com/ru/companies/bothub/news/888370/",
                "https://rutracker.org/forum/viewforum.php?f=1958"
            };

            //var id = "01977f7a-4eed-794d-b049-5b6a3f904cc6";
            //////////////////////


            //var order = OrderToScrape.Create(urls);
            //if (order.IsFailure)
            //    return order.Error;

            //await _orderRepository.Add(order.Value, ct);

            //await _unitOfWork.SaveChangesAsync(ct);

            //_logger.LogInformation("Добавлен ордер, ID: {id}", order.Value.Id);



            ///////////////////
            await _scrapingJob.ProcessAsync(Guid.Parse("0197821f-a090-7bf7-84b6-839fd2a373bd"));
            /////////////////

            return true;
        }


    }
}
