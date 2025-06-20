namespace Scraper.Domain.Common;

/// <summary>
/// Ошибка
/// </summary>
public class Error
{
    /// <summary>
    /// Разделитель string
    /// </summary>
    private const string SEPARATOR = "||";

    /// <summary>
    /// Пустая ошибка
    /// </summary>
    public static readonly Error None = new(String.Empty, String.Empty);

    /// <summary>
    /// Код ошибки
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="code"> Код </param>
    /// <param name="message"> Сообщение </param>
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Сериализация ошибки
    /// </summary>
    /// <returns></returns>
    public string Serialize()
    {
        return $"{Code}{SEPARATOR}{Message}";
    }

    /// <summary>
    /// Десериализация ошибки
    /// </summary>
    /// <param name="serialized"> Сериализованные данные </param>
    /// <returns></returns>
    public static Error Deserialize(string serialized)
    {
        var data = serialized.Split([SEPARATOR], StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 2)
            return new Error("unexpected", data[0]);

        return new(data[0], data[1]);
    }
}
