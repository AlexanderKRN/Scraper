using Scraper.Domain.Common;

namespace Scraper.API.Common;

/// <summary>
/// Шаблон ответа на запросы API
/// </summary>
public class Envelope
{
    /// <summary>
    /// Результат
    /// </summary>
    public object? Result { get; }

    /// <summary>
    /// Перечень ошибок
    /// </summary>
    public List<ErrorInfo>? ErrorInfo { get; }

    /// <summary>
    /// Время
    /// </summary>
    public DateTime TimeGenerated { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result"> Результат </param>
    /// <param name="errors"> Перечень ошибок </param>
    private Envelope(object? result, IEnumerable<ErrorInfo>? errors)
    {
        Result = result;
        ErrorInfo = errors?.ToList();
        TimeGenerated = DateTime.Now;
    }

    /// <summary>
    /// Шаблон ответ успешной обработки
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Envelope Ok(object? result = null)
    {
        return new(result, null);
    }

    /// <summary>
    /// Шаблон ответа обработки с ошибкой
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static Envelope Error(params ErrorInfo[] errors)
    {
        return new(null, errors);
    }
}

/// <summary>
/// Шаблон ошибки
/// </summary>
public class ErrorInfo
{
    /// <summary>
    /// Кодаошибки
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// Ошибочное поле
    /// </summary>
    public string? InvalidField { get; }

    public ErrorInfo(Error? error, string? invalidField = null)
    {
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        InvalidField = invalidField;
    }
}
