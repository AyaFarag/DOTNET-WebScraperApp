using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services.BackgroundJobs;
using Infrastructure.Services.Scraping;
using Ingestion.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Queries.Ingestion;
using Shared.Infrastructure.Data;



namespace Ingestion.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIngestionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
  
            services.AddScoped<IScraper, PlaywrightScraper>();
            services.AddScoped<IJobScheduler, HangfireJobScheduler>();
            services.AddScoped<ScrapingJob>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<IRawPriceRepository, RawPriceRepository>();
            services.AddScoped<IUnitOfWork, IngestionUnitOfWork>();
            services.AddScoped<IIngestionExecutionRepository,IngestionExecutionRepository>();
            services.AddScoped<IRawPriceQueryReader, RawPriceQueryReader>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<OutboxPublisherJob>();

            services.AddDbContext<IngestionDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


            return services;
        }
    }
}
