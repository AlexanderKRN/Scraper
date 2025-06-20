using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.DbContexts;

/// <summary>
/// Контекст базы данных для чтения
/// </summary>
public class ScraperReadDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration"> Конфигурация </param>
    public ScraperReadDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Ордера
    /// </summary>
    public DbSet<OrderToScrape> Orders => Set<OrderToScrape>();

    /// <summary>
    /// Конфигурация контекста
    /// </summary>
    /// <param name="optionsBuilder"> Конфигуратор опций </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Scraper"));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    /// <summary>
    /// Конфигурация чтения
    /// </summary>
    /// <param name="modelBuilder"> Конфигуратор опций </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ScraperReadDbContext).Assembly,
            type => type.FullName?.Contains("DbConfiguration.Read") ?? false);
    }
}