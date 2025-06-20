using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Features.ScrapingByOder;

namespace Scraper.Application;

/// <summary>
/// Добавление сервисов в DI
/// </summary>
public static class DependencyRegistration
{
    /// <summary>
    /// Сервисы слоя бизнес-логики приложения
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();

        return services;
    }

    /// <summary>
    /// Сервисы обработчиков
    /// </summary>
    /// <param name="services"> Сервисы </param>
    /// <returns></returns>
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<SetOrderHandler>();

        return services;
    }
}
