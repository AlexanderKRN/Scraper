using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Features.ScrapingByOder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
