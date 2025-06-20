namespace Scraper.Domain.Common;

/// <summary>
/// Перечень кастомных ошибок
/// </summary>
public static class ErrorList
{
    /// <summary>
    /// Перечень основных ошибок
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Внутренняя ошибка
        /// </summary>
        /// <param name="message"> Сообщение </param>
        /// <returns></returns>
        public static Error Internal(string message)
            => new("internal", message);

        /// <summary>
        /// Непредвиденная ошибка
        /// </summary>
        /// <returns></returns>
        public static Error Unexpected()
            => new("unexpected", "unexpected");

        /// <summary>
        /// Ресурс не найден по Id
        /// </summary>
        /// <param name="id"> Уникальный номер </param>
        /// <returns></returns>
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return new("record.not.found", $"Запись не существует{forId}");
        }

        /// <summary>
        /// Ошибка при сборе данных по Id
        /// </summary>
        /// <param name="id"> Уникальный номер </param>
        /// <returns></returns>
        public static Error WebScraperFault(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return new("web.scraper.fault", $"Ошибка при работе Web scraper{forId}");
        }

        /// <summary>
        /// Значение ошибочно
        /// </summary>
        /// <param name="name"> Название </param>
        /// <returns></returns>
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "Значение";
            return new("value.is.invalid", $"{label} ошибочно");
        }

        /// <summary>
        /// Превышение ограничения по размеру
        /// </summary>
        /// <returns></returns>
        public static Error DataSizeInvalid()
        {
            return new("invalid.data.size", "Превышен размер данных в 5 мБ");
        }
    }
}
