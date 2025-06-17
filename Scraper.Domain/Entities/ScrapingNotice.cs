using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using Scraper.Domain.ValueObject;

namespace Scraper.Domain.Entities
{
    public class ScrapingNotice
    {
        public Guid Id { get; }
        public string Url { get; private set; } = null!;
        public string ErrorScraping { get; set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public Headers Headers { get; private set; }

        private ScrapingNotice()
        {
        }

        private ScrapingNotice(string url, Headers headers)
        {
            Id = Guid.NewGuid();
            Url = url;
            Headers = headers;
            CreatedAt = DateTime.UtcNow;
        }

        public static Result<ScrapingNotice, Error> Create(
            string url,
            Headers data)
        {
            if (url is null)
                return ErrorList.General.ValueIsInvalid();

            return new ScrapingNotice(url, data);
        }
    }
}
