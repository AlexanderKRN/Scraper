using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Features.ScrapingByOder;

namespace Scraper.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddHandlers();

            return services;
        }

        private static IServiceCollection AddHandlers(
            this IServiceCollection services)
        {
            services.AddScoped<SetOrderHandler>();

            return services;
        }
    }
}
