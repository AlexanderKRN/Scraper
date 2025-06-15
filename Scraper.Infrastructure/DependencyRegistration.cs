using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Providers;
using Scraper.Infrastructure.DbContexts;
using Scraper.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDataStorages(configuration)
                .AddProviders();

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
    }
}
