using Infrastructure.Services.Scraping;
using Ingestion.Application.Interfaces;
using Ingestion.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIngestionInfrastructure(this IServiceCollection services)
        {
  
            services.AddScoped<IScraper, PlaywrightScraper>();

            return services;
        }
    }
}
