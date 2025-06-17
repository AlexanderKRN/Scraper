using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Providers;
using Scraper.Infrastructure.DbContexts;
using Scraper.Infrastructure.Jobs;
using Scraper.Infrastructure.Providers;

namespace Scraper.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDataStorages(configuration)
                .AddProviders()
                .AddJobs();

            return services;
        }

        private static IServiceCollection AddDataStorages(
            this IServiceCollection services, IConfiguration configuration)
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
    }
}
