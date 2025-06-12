using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scraper.Domain.Entities;

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

            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "Значение";
                return new("value.is.invalid", $"{label} ошибочно");
            }

            public static Error FileSizeInvalid()
            {
                return new("invalid.file.size", "Превышен размер файла в 5 мБ");
            }

        }
    }
}
