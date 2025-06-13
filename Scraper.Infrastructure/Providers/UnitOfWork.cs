using Scraper.Application.Providers;
using Scraper.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure.Providers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScraperWriteDbContext _dbContext;

        public UnitOfWork(ScraperWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return await _dbContext.SaveChangesAsync(ct);
        }
    }
}
