using Analytics.Application.Interfaces.AI.AnalyticsSearch;
using Analytics.Application.Interfaces.AI.AnomalyExplanation;
using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using Analytics.Application.Interfaces.AI.SearchQuery;
using Analytics.Application.Interfaces.Anomalies.Repository;
using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Application.Interfaces.Weights.Repository;
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
            services.AddScoped<IPriceRelativeRepository, PriceRelativeRepository>();
            services.AddScoped<IPriceWeightRepository,PriceWeightRepository>();
            services.AddScoped<IWeightedPriceRepository, WeightedPriceRepository>();
            services.AddScoped<ICategoryIndexRepository,CategoryIndexRepository>();
            services.AddScoped<ILaspeyresIndexRepository, LaspeyresIndexRepository>();
            services.AddScoped<IPaascheIndexRepository, PaascheIndexRepository>();
            services.AddScoped<IFisherIndexRepository, FisherIndexRepository>();
            services.AddScoped<ICpiWeightRepository, CpiWeightRepository>();
            services.AddScoped<IConsumerPriceIndexRepository, ConsumerPriceIndexRepository>();
            services.AddScoped<IPpiWeightRepository, PpiWeightRepository>();
            services.AddScoped<IProducerPriceIndexRepository, ProducerPriceIndexRepository>();

            services.AddScoped<IAnalyticsSearchQueryAgent, AnalyticsSearchAgent>();
            services.AddScoped<IPriceSnapshotSearchAgent, PriceSnapshotSearchAgent>();
            services.AddScoped<IPriceSnapshotSearchReader, AzurePriceSnapshotSearchReader>();


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


            var azureSearchOptions = configuration
                    .GetSection(AzureSearchOptions.SectionName)
                    .Get<AzureSearchOptions>()
                ?? throw new InvalidOperationException(
                    "AzureSearch configuration is missing.");

            services.AddSingleton<SearchClient>(sp =>
            {
                return new SearchClient(
                    new Uri(azureSearchOptions.Endpoint),
                    azureSearchOptions.PriceSnapshotIndexName,
                    new AzureKeyCredential(
                        azureSearchOptions.ApiKey));
            });

            return services;
        }
    }
}
