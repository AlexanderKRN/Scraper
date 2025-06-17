using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scraper.Infrastructure.ReadModels;

namespace Scraper.Infrastructure.DbContexts
{
    public class ScraperReadDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ScraperReadDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<OrderReadModel> Orders => Set<OrderReadModel>();
        public DbSet<NoticeReadModel> Notices => Set<NoticeReadModel>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Scraper"));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ScraperReadDbContext).Assembly,
                type => type.FullName?.Contains("DbConfiguration.Read") ?? false);
        }
    }
}
