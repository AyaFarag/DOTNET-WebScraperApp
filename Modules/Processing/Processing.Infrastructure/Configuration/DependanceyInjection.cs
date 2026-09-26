using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Application.Interfaces.Repositories;
using Processing.Infrastructure.Presistance.Repository;
using Shared.Contracts.Queries.Processing;

namespace Processing.Infrastructure.Configuration
{
    public static class DependanceyInjection
    {
        public static IServiceCollection AddProcessingInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add your infrastructure services here
            // For example, you can add database context, repositories, etc.
           // services.AddScoped<IProcessedProductReader , ProcessedProductReader>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IUnitOfWork, ProcessingUnitOfWork>();
            services.AddScoped<IProcessedPriceRepository, ProcessedPriceRepository>();
            services.AddScoped(typeof(IProcessedProductReader<>), typeof(ProcessedProductReader<>));
;

            return services;
        }
    }
}
