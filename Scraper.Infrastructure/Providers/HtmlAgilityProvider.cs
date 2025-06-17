using CSharpFunctionalExtensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using Attribute = Scraper.Domain.ValueObject.Attribute;
using System.Text.RegularExpressions;
using Scraper.Domain.ValueObject;
using Scraper.Application.Providers;

namespace Scraper.Infrastructure.Providers;

public class HtmlAgilityProvider : IHtmlAgilityProvider
{
    public const string ATTRIBUTE_PATTERN = @"[][\w-]+=""[^""]+";
    public const string ATTRIBUTE_SEPARATOR = "=\"";

    private readonly ILogger<HtmlAgilityProvider> _logger;

    public HtmlAgilityProvider(ILogger<HtmlAgilityProvider> logger)
    {
        _logger = logger;
    }

    public async Task<Result<ScrapingNotice, Error>> GetDataByUrl(string url, CancellationToken ct)
    {
        try
        {
            var web = new HtmlWeb();

            var title = await GetNode(web, url, Constants.ScrapinConstants.TITLE_XPATH);

            var metaAttributes = await GetNodeList(web, url, Constants.ScrapinConstants.META_XPATH);

            List<MetaLine> metaLines = [];

            foreach (var nodeAttribute in metaAttributes.Value ?? [])
            {
                List<Attribute> attributes = [];

                var error = string.Empty;

                foreach (Match match in Regex.Matches(
                    nodeAttribute.OuterHtml ?? string.Empty,
                    ATTRIBUTE_PATTERN))
                {
                    var data = match.Value.Split(ATTRIBUTE_SEPARATOR);

                    var attribute = Attribute.Create(data[0], data[1]);
                    if (attribute.IsFailure)
                        return attribute.Error;

                    attributes.Add(attribute.Value);
                };

                var metaLine = MetaLine.Create(attributes);
                if (metaLine.IsFailure)
                    return metaLine.Error;

                metaLines.Add(metaLine.Value);
            }

            var headers = Headers.Create(title.Value, metaLines);
            if (headers.IsFailure)
                return headers.Error;

            var notice = ScrapingNotice.Create(url, headers.Value);
            if (notice.IsFailure)
                return notice.Error;

            return notice.Value;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при работе HtmlAgility: {message}", e.Message);
            return ErrorList.General.NotFound();
        }
    }

    private async Task<Result<string, Error>> GetNode(
        HtmlWeb? web, string url, string xPath)
    {
        var doc = web?.Load(url);

        var node = doc?.DocumentNode
            .SelectSingleNode(xPath);

        var nodeLine = node?.OuterHtml;

        var nodeData = "TITLE"; ;// "TITLE"; // REGEX.MATCH----------------------------------------------------
        if (nodeData is null)
            return ErrorList.General.ValueIsInvalid();

        return nodeData;
    }

    private async Task<Result<List<HtmlNode>, Error>> GetNodeList(
        HtmlWeb? web, string url, string xPath)
    {
        var doc = web?.Load(url);

        var nodes = doc?.DocumentNode.SelectNodes(xPath);

        var nodesData = nodes?.ToList();
        if (nodesData is null)
            return ErrorList.General.ValueIsInvalid();

        return nodesData;
    }
}
