using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace BuildingBlocks.Observability.Metrics;

public static class PrometheusExtensions
{
    public static IServiceCollection AddMetricsData(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseMetrics(this IApplicationBuilder app)
    {
        app.UseHttpMetrics();
        //app.MapMetrics(); // /metrics

        return app;
    }
}