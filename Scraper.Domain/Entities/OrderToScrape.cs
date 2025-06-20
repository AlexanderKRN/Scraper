using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.Entities;

/// <summary>
/// Ордер сбора данных
/// </summary>
public class OrderToScrape
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Перечень URL-адресов ордера с ограничением добавления
    /// </summary>
    public IReadOnlyList<string> Urls => _urls;
    private readonly List<string> _urls = [];

    /// <summary>
    /// Перечень отчётов с ограничением добавления
    /// </summary>
    public IReadOnlyList<ScrapingNotice> Notices => _notices;
    private readonly List<ScrapingNotice> _notices = [];

    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    private OrderToScrape()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="urls"> Перечень URL-адресов </param>
    private OrderToScrape(IEnumerable<string> urls)
    {
        _urls = urls.ToList();
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Добавление отчёта
    /// </summary>
    /// <param name="notice"> отчёт </param>
    /// <returns></returns>
    public Result<bool, Error> AddNotice(ScrapingNotice notice)
    {
        _notices.Add(notice);
        return true;
    }

    /// <summary>
    /// Создание нового отчёта
    /// </summary>
    /// <param name="urls"> Перечень URL-адресов </param>
    /// <returns></returns>
    public static Result<OrderToScrape, Error> Create (
        IEnumerable<string> urls)
    {
        var listPathes = urls.ToList();
        if (listPathes.Count >= Constraints.LINKS_COUNT_LIMIT)
            return ErrorList.General.ValueIsInvalid();

        return new OrderToScrape(urls);
    }
}
