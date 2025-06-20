using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using Scraper.Domain.ValueObject;

namespace Scraper.Domain.Entities;

/// <summary>
/// Отчет с собранными данными
/// </summary>
public class ScrapingNotice
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public string Url { get; private set; }

    /// <summary>
    /// Ошибка сбора данных
    /// </summary>
    public string? ErrorScraping { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Данные тега "headers"
    /// </summary>
    public Headers? Headers { get; private set; }

    /// <summary>
    /// Конструктор EF Core
    /// </summary>
    private ScrapingNotice()
    {
    }

    /// <summary>
    /// Конструктор 
    /// </summary>
    /// <param name="url"> Перечень URL-адресов </param>
    /// <param name="headers"> Данные тега "headers" </param>
    private ScrapingNotice(string url, Headers? headers)
    {
        Url = url;
        Headers = headers;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Создание нового отчёта
    /// </summary>
    /// <param name="url"> Перечень URL-адресов </param>
    /// <param name="headers"> Данные тега "headers" </param>
    /// <returns></returns>
    public static Result<ScrapingNotice, Error> Create(
        string url,
        Headers? headers)
    {
        if (url is null)
            return ErrorList.General.ValueIsInvalid();

        return new ScrapingNotice(url, headers);
    }
}
