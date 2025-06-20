using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Features;
using Scraper.Application.Providers;
using Scraper.Infrastructure.DbContexts;
using Scraper.Infrastructure.Jobs;
using Scraper.Infrastructure.Providers;
using Scraper.Infrastructure.Queries.Notices;
using Scraper.Infrastructure.Repository;

namespace Scraper.Infrastructure;

/// <summary>
/// Добавление сервисов в DI
/// </summary>
public static class DependencyRegistration
{
    /// <summary>
    /// Сервисы слоя инфраструктуры приложения
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <param name="configuration"> Конфигурация </param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDataStorages()
            .AddProviders()
            .AddJobs()
            .AddRepositories()
            .AddQueries()
            .AddHangfire(configuration);

        return services;
    }

    /// <summary>
    /// Сервисы работы с базой данных
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <returns></returns>
    private static IServiceCollection AddDataStorages(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ScraperWriteDbContext>();
        services.AddScoped<ScraperReadDbContext>();

        return services;
    }

    /// <summary>
    /// Сервисы провайдеров
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <returns></returns>
    private static IServiceCollection AddProviders(
        this IServiceCollection services)
    {
        services.AddScoped<IHtmlAgilityProvider, HtmlAgilityProvider>();

        return services;
    }

    /// <summary>
    /// Сервисы задач
    /// </summary>
    /// <param name="services">  Сервисы  </param>
    /// <returns></returns>
    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddScoped<IScrapingJob, ScrapingJob>();

        return services;
    }

    /// <summary>
    /// Сервис библиотеки Hangfire
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <param name="configuration"> Конфигурация </param>
    /// <returns></returns>
    private static IServiceCollection AddHangfire(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(configuration.GetConnectionString("Scraper"))));

        services.AddHangfireServer(options =>
            options.SchedulePollingInterval = TimeSpan.FromSeconds(20));

        return services;
    }

    /// <summary>
    /// Сервисы репозиториев
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <returns></returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    /// <summary>
    /// Сервисы запросов к базе данных
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <returns></returns>
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<GetNoticesQuery>();

        return services;
    }
}
