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
        public DateTime CreatedAt { get; private set; }
        public ScrapingData Data { get; private set; } = null!;

        private ScrapingNotice()
        {
        }

        private ScrapingNotice(ScrapingData data)
        {
            Data = data;
            CreatedAt = DateTime.UtcNow;
        }

        public static Result<ScrapingNotice, Error> Create(
            )
        {

        }
    }
