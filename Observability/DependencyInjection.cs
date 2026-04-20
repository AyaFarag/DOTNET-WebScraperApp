using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Observability;

public static class DependencyInjection
{
    public static IServiceCollection AddObservabilityModule(this IServiceCollection services, IConfiguration config)
    {
        return Extensions.ObservabilityExtensions.AddObservability(services, config);
    }

    public static IApplicationBuilder UseObservabilityModule(this IApplicationBuilder app)
    {
        return Extensions.ObservabilityExtensions.UseObservability(app);
    }
}
