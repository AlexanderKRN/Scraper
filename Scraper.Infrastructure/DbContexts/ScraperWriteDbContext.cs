using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.DbContexts;

/// <summary>
/// Контекст базы данных для записи
/// </summary>
public class ScraperWriteDbContext : DbContext, IUnitOfWork
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration"> Конфигурация </param>
    public ScraperWriteDbContext(IConfiguration configuration)
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
    }

    /// <summary>
    /// Конфигурация записи
    /// </summary>
    /// <param name="modelBuilder"> Конфигуратор опций </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ScraperWriteDbContext).Assembly,
            type => type.FullName?.Contains("DbConfiguration.Write") ?? false);
    }
}
