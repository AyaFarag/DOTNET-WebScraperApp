using API.Scenario.Indexing.Interface;
using API.Scenario.Indexing.Service;
using API.Scenario.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory; // Ensure the NuGet package Microsoft.EntityFrameworkCore.InMemory is installed

namespace API.Scenario.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddScenarioConfigurations(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("PriceDb"));

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<Program>(); });
            services.AddScoped<IIndexService, IndexService>();
            return services;
        }
    }
}
