using CSharpFunctionalExtensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Scraper.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure.Providers
{
    public class HtmlAgilityProvider : IHtmlAgilityProvider
    {
        private readonly ILogger<HtmlAgilityProvider> _logger;

        public HtmlAgilityProvider(ILogger<HtmlAgilityProvider> logger)
        {
            _logger = logger;
        }

        public async Task<Result<bool, Error>> GetData(CancellationToken ct)
        {
            var url = "https://habr.com/ru/companies/bothub/news/888370/";
            var titleXPAth = "/html/head/title";

            var web = new HtmlWeb();

            var doc = web.Load(url);

            var node = doc.DocumentNode.SelectSingleNode(titleXPAth);

            return true;
        }
    }
}
