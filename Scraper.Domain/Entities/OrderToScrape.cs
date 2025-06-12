using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Domain.Entities
{
    public class OrderToScrape
    {
        public const string TXT = "txt";

        public Guid Id { get; }
        public DateTime CreatedAt { get; private set; }

        public IReadOnlyList<string> Paths => _paths;
        private readonly List<string> _paths = [];

        public IReadOnlyList<ScrapingNotice> Notices => _notices;
        private readonly List<ScrapingNotice> _notices = [];

        private OrderToScrape()
        {
        }

        private OrderToScrape()
        {
            
        }

    }
}
