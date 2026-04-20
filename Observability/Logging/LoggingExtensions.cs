using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace BuildingBlocks.Observability.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddLoggingModule(this IServiceCollection services, IConfiguration config)
    {
        var elasticUrl = config["Elastic:Url"];
        if (string.IsNullOrWhiteSpace(elasticUrl))
        {
            throw new InvalidOperationException("Elastic:Url configuration value is missing or empty.");
        }

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            //.WriteTo.Elasticsearch(elasticUrl))
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl)))
            .CreateLogger();

        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSerilog();
        });

        return services;
    }
}