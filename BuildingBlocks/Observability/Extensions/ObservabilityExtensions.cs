using BuildingBlocks.Observability.Abstractions.Auditing;
using BuildingBlocks.Observability.Abstractions.Correlation;
using BuildingBlocks.Observability.Auditing;
using BuildingBlocks.Observability.Correlation;
using BuildingBlocks.Observability.Logging;
using BuildingBlocks.Observability.Metrics;
using BuildingBlocks.Observability.Tracing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Observability.Extensions;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<ICorrelationContext, CorrelationContext>();
        services.AddSingleton<IAuditService, AuditService>();

        services.AddLoggingModule(config);
        services.AddMetricsData();
        services.AddTracing(config);

        return services;
    }

    public static IApplicationBuilder UseObservability(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMetrics();

        return app;
    }
}