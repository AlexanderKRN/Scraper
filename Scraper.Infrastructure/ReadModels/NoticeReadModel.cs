using Scraper.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure.ReadModels
{
    public class NoticeReadModel
    {
        public Guid Id { get; init; }
        public string Url { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string Data { get; init; } = string.Empty;
    }
}
