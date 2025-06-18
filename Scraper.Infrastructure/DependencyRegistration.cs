using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Features;
using Scraper.Application.Providers;
using Scraper.Infrastructure.DbContexts;
using Scraper.Infrastructure.Jobs;
using Scraper.Infrastructure.Providers;
using Scraper.Infrastructure.Repository;

namespace Scraper.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddDataStorages()
                .AddProviders()
                .AddJobs()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddDataStorages(
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ScraperWriteDbContext>();
            services.AddScoped<ScraperReadDbContext>();

            return services;
        }

        private static IServiceCollection AddProviders(
            this IServiceCollection services)
        {
            services.AddScoped<IHtmlAgilityProvider, HtmlAgilityProvider>();

            return services;
        }

        private static IServiceCollection AddJobs(this IServiceCollection services)
        {
            services.AddScoped<IScrapingJob, ScrapingJob>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
