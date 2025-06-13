using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Entities;
using Scraper.Infrastructure.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure.DbContexts
{
    public class ScraperWriteDbContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public ScraperWriteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<OrderToScrape> Orders => Set<OrderToScrape>();
        public DbSet<ScrapingNotice> Notices => Set<ScrapingNotice>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Scraper"));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ScraperWriteDbContext).Assembly,
                type => type.FullName?.Contains("DbConfiguration.Write") ?? false);
        }
    }
}
