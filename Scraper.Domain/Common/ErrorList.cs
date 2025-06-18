namespace Scraper.Domain.Common
{
    public static class ErrorList
    {
        public static class General
        {
            public static Error Internal(string message)
                => new("internal", message);

            public static Error Unexpected()
                => new("unexpected", "unexpected");

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id '{id}'";
                return new("record.not.found", $"Запись не существует{forId}");
            }

            public static Error WebScraperFault(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id '{id}'";
                return new("web.scraper.fault", $"Ошибка при работе Web scraper{forId}");
            }

            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "Значение";
                return new("value.is.invalid", $"{label} ошибочно");
            }

            public static Error DataSizeInvalid()
            {
                return new("invalid.data.size", "Превышен размер данных в 5 мБ");
            }

        }
    }
}
