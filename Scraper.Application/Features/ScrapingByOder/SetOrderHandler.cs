using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Common;
using Scraper.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Application.Features.ScrapingByOder
{
    public class SetOrderHandler
    {
        private readonly IHtmlAgilityProvider _agilityProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SetOrderHandler> _logger;

        public SetOrderHandler(
            IHtmlAgilityProvider agilityProvider,
            IUnitOfWork unitOfWork,
            ILogger<SetOrderHandler> logger)
        {
            _agilityProvider = agilityProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<bool, Error>> Handle(CancellationToken ct)
        {
            var data =_agilityProvider.GetData(ct);
            

            return true;
        }


    }
}
