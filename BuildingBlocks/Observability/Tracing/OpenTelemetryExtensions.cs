using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

namespace BuildingBlocks.Observability.Tracing;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration config)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
                    //.AddConsoleExporter(); // later replace
            });

        return services;
    }
}
