using CSharpFunctionalExtensions;
using Scraper.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Domain.Entities
{
    public class OrderToScrape
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; private set; }

        public IReadOnlyList<string> Paths => _paths;
        private readonly List<string> _paths = [];

        public IReadOnlyList<ScrapingNotice> Notices => _notices;
        private readonly List<ScrapingNotice> _notices = [];

        private OrderToScrape()
        {
        }

        private OrderToScrape(IEnumerable<string> paths)
        {
            _paths = paths.ToList();
            CreatedAt = DateTime.UtcNow;
        }

        public Result<bool, Error> AddNotice(ScrapingNotice notice)
        {
            _notices.Add(notice);
            return true;
        }

        public static Result<OrderToScrape, Error> Create (
            IEnumerable<string> paths)
        {
            var listPathes = paths.ToList();
            if (listPathes.Count >= Constraints.LINKS_COUNT_LIMIT)
                return ErrorList.General.ValueIsInvalid();

            return new OrderToScrape(paths);
        }

    }
}
