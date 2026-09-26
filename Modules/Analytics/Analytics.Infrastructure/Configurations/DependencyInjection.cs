using Analytics.Application.Interfaces.AI.Agent;
using Analytics.Application.Interfaces.AI.Repository;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Infrastructure.Persistence.Repositories;
using Analytics.Infrastructure.Persistence.Repositories.AI.Agent;
using Analytics.Infrastructure.Persistence.Repositories.AI.Repository;
using Analytics.Infrastructure.Persistence.Repositories.AI.Service;
using Analytics.Infrastructure.Persistence.Service.Idempotency;
using Analytics.Infrastructure.Persistence.Service.Outbox;
using Analytics.Infrastructure.Persistence.Service.Uow;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Analytics.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IPriceSnapshotRepository, PriceSnapshotRepository>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IAnalyticsUnitOfWork, AnalyticsUnitOfWork>();
            services.AddScoped<IPriceAnomalyRepository, PriceAnomalyRepository>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IAnalyticsDashboardRepository, AnalyticsDashboardRepository>();
            services.AddScoped<IAnalyticsExecutionRepository, AnalyticsExecutionRepository>();
            services.AddScoped<IAnomalyExplanationRepository, AnomalyExplanationRepository>();
            services.AddScoped<IAnomalyExplanationAgent,AnomalyExplanationAgent>();
            services.AddScoped<ISearchQueryAgent,SearchQueryAgent>();

            services.AddScoped<IPriceSnapshotSearchIndexer, AzurePriceSnapshotSearchIndexer>();
            services.AddSingleton<IPriceSnapshotSearchIndexInitializer, PriceSnapshotSearchIndexInitializer>();



            services.Configure<AzureSearchOptions>(
                configuration.GetSection(
                    AzureSearchOptions.SectionName));



            services.AddSingleton<SearchIndexClient>(sp =>
            {
                var options =
                    sp.GetRequiredService<
                        IOptions<AzureSearchOptions>>()
                    .Value;

                return new SearchIndexClient(
                    new Uri(options.Endpoint),
                    new AzureKeyCredential(options.ApiKey));
            });

            services.AddSingleton<SearchClient>(sp =>
            {
                var options =
                    sp.GetRequiredService<
                        IOptions<AzureSearchOptions>>()
                    .Value;

                return new SearchClient(
                    new Uri(options.Endpoint),
                    options.PriceSnapshotIndexName,
                    new AzureKeyCredential(options.ApiKey));
            });



            services.AddSingleton<IChatCompletionService>(sp =>
            {
                var configuration =
                    sp.GetRequiredService<IConfiguration>();

                var apiKey =
                    configuration["OpenAI:ApiKey"]
                    ?? throw new InvalidOperationException(
                        "OpenAI API key is not configured.");

                var model =
                    configuration["OpenAI:Model"]
                    ?? throw new InvalidOperationException(
                        "OpenAI model is not configured.");

                return new OpenAIChatCompletionService(
                    modelId: model,
                    apiKey: apiKey);
            });

            return services;
        }
    }
}
