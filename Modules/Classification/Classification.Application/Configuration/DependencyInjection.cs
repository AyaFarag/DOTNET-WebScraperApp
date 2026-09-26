using Classification.Application.Interfaces.Service;
using Classification.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Queries.Processing;

namespace Classification.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddClassificationApplication(this IServiceCollection services)
        {
            // Register application services, handlers, and other dependencies here
            //Example: services.AddTransient<IClassificationAgent, ClassificationAgent>();
            services.AddScoped<IClassificationService,ClassificationService>();
            

            return services;
        }
    }
}
