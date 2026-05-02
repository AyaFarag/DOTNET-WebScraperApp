using Application.CQRS.Comand;
using Ingestion.Application.CQRS.Query;
using Ingestion.Application.Interfaces;
using Ingestion.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Configurations;

    public static class DependencyInjection
    {
        public static IServiceCollection AddIngestionApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ScrapePricesCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ScrapePricesQuery).Assembly);
            });

            services.AddMediatR(cfg =>
              cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddScoped<IngestionService>();

             return services;
        }
    }
