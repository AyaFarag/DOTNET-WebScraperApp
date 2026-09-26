using Index.Application.EventHandler;
using Index.Application.Interfaces.Search;
using Index.Application.Interfaces.Service;
using Index.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Index.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIndexingApplication(this IServiceCollection services) 
        {
            services.AddScoped<IIndexingService, IndexingService>();
            services.AddScoped<IProductSearchService,ProductSearchService>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(
                    typeof(PriceDataClassifiedEventHandler)
                        .Assembly);
            });
            return services;
        }
    }
}
