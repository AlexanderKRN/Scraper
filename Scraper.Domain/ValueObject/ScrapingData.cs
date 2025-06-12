using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject
{
    public class ScrapingData
    {
        public string Head { get; private set; } = null!;
        public string Title { get; private set; } = null!;
        public string Meta { get; private set; } = null!;

        private ScrapingData(string head, string title, string meta)
        {
            Head = head;
            Title = title;
            Meta = meta;
        }

        public static Result<ScrapingData, Error> Create(
            string head, string title, string meta)
        {
            head = head.Trim();
            if (head.IsEmpty())
                return ErrorList.General.ValueIsInvalid();

            return new ScrapingData(head, title, meta);
        }
    }
}
