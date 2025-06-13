using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using Scraper.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Domain.Entities
{
    public class ScrapingNotice
    {
        public Guid Id { get; }
        public string Url { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public ScrapingData Data { get; private set; } = null!;

        private ScrapingNotice()
        {
        }

        private ScrapingNotice(string url, ScrapingData data)
        {
            Url = url;
            Data = data;
            CreatedAt = DateTime.UtcNow;
        }

        public static Result<ScrapingNotice, Error> Create(
            string url,
            ScrapingData data,
            long lenght)
        {
            if (lenght > Constraints.MAX_DATA_SIZE)
                return ErrorList.General.DataSizeInvalid();

            return new ScrapingNotice(url, data);
        }
    }
}
