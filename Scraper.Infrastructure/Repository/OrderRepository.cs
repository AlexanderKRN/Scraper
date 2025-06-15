using Scraper.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure.Repository
{
    public class OrderRepository
    {
        private readonly ScraperWriteDbContext _dbContext;

        public OrderRepository(ScraperWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
