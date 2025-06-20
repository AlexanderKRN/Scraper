using CSharpFunctionalExtensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using Attribute = Scraper.Domain.ValueObject.Attribute;
using System.Text.RegularExpressions;
using Scraper.Domain.ValueObject;
using Scraper.Application.Providers;
using Scraper.Infrastructure.Common;

namespace Scraper.Infrastructure.Providers;

/// <summary>
/// Провйдер функциональности библиотеки HtmlAgility
/// </summary>
public class HtmlAgilityProvider : IHtmlAgilityProvider
{
    #region Постоянные величины для обработки Regex
    /// <summary>
    /// Regex паттерн первичной выборки тега "title"
    /// </summary>
    public const string TITLE_PATTERN_MAIN = @"[>][^<]+";

    /// <summary>
    /// Regex паттерн вторичной выборки тега "title"
    /// </summary>
    public const string TITLE_PATTERN_SLAVE = @"[^>]+";

    /// <summary>
    /// Regex паттерн выборки аттрибутов
    /// </summary>
    public const string ATTRIBUTE_PATTERN = @"[][\w-]+=""[^""]+";

    /// <summary>
    /// Regex паттерн разделения названия и значения аттрибута
    /// </summary>
    public const string ATTRIBUTE_SEPARATOR = "=\"";
    #endregion

    private readonly ILogger<HtmlAgilityProvider> _logger;

    /// <summary>
    /// Конструтор
    /// </summary>
    /// <param name="logger"> Регистратор </param>
    public HtmlAgilityProvider(ILogger<HtmlAgilityProvider> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Получение данных сайта по URL-адресу
    /// </summary>
    /// <param name="url"> URL-адрес </param>
    /// <param name="ct"> Токен отмены </param>
    /// <returns></returns>
    public async Task<Result<ScrapingNotice, Error>> GetDataByUrl(
        string url,
        CancellationToken ct)
    {
        try
        {
            var web = new HtmlWeb();

            var title = await GetNode(
                web,
                url,
                ScrapinConstants.TITLE_XPATH);

            var metaAttributes = await GetNodeList(
                web,
                url,
                ScrapinConstants.META_XPATH);

            List<MetaLine> metaLines = [];

            foreach (var nodeAttribute in metaAttributes.Value ?? [])
            {
                List<Attribute> attributes = [];

                var error = string.Empty;

                #region Обработка аттрибутов
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
                #endregion

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
            return ErrorList.General.WebScraperFault();
        }
    }

    #region Метод GetNode

    /// <summary>
    /// Полчение данных одного элемента сайта
    /// </summary>
    /// <param name="web"> Экземпляр HtmlWeb </param>
    /// <param name="url"> URL-адрес </param>
    /// <param name="xPath"> XPath путь к метаданным </param>
    /// <returns></returns>
    private async Task<Result<string, Error>> GetNode(
        HtmlWeb? web, string url, string xPath)
    {
        var doc = web?.Load(url);

        var node = doc?.DocumentNode
            .SelectSingleNode(xPath);

        var nodeLine = node?.OuterHtml;

        var nodeDataMain = Regex.Match(nodeLine?? string.Empty, TITLE_PATTERN_MAIN);
        var nodeDataSlave = Regex.Match(nodeDataMain.Value?? string.Empty, TITLE_PATTERN_SLAVE);
        if (nodeDataSlave.Value is null)
            return ErrorList.General.WebScraperFault();

        return nodeDataSlave.Value;
    }
    #endregion

    #region Метод GetNodeList 
    /// <summary>
    /// Полчение данных перечня элементов сайта
    /// </summary>
    /// <param name="web"> Экземпляр HtmlWeb </param>
    /// <param name="url"> URL-адрес </param>
    /// <param name="xPath"> XPath путь к метаданным </param>
    /// <returns></returns>
    private async Task<Result<List<HtmlNode>, Error>> GetNodeList(
        HtmlWeb? web, string url, string xPath)
    {
        var doc = web?.Load(url);

        var nodes = doc?.DocumentNode.SelectNodes(xPath);

        var nodesData = nodes?.ToList();
        if (nodesData is null)
            return ErrorList.General.WebScraperFault();

        return nodesData;
    }
    #endregion
}
