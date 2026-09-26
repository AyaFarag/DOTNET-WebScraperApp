using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Index.Application.Interfaces;
using Index.Application.Interfaces.Repository;
using Index.Application.Interfaces.Search;
using Index.Infrastructure.Presistance.Repositories;
using Index.Infrastructure.Presistance.Repositories.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Queries.Indexing;


namespace Index.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIndexInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            services.Configure<AzureSearchOptions>(
            configuration.GetSection(
            AzureSearchOptions.SectionName));

            var options = configuration
                    .GetSection(AzureSearchOptions.SectionName)
                    .Get<AzureSearchOptions>()
                ?? throw new InvalidOperationException(
                    "Azure Search configuration is missing.");

            services.AddSingleton<SearchClient>(_ =>
            {
                return new SearchClient(
                    new Uri(options.Endpoint),
                    options.IndexName,
                    new AzureKeyCredential(
                        options.ApiKey));
            });

            services.AddSingleton<SearchIndexClient>(_ =>
            {
                return new SearchIndexClient(
                    new Uri(options.Endpoint),
                    new AzureKeyCredential(options.ApiKey));
            });

            services.AddScoped( typeof(IIndexedProductReader<>) , typeof(IndexedProductReader<>));
            services.AddScoped<ISearchQueryPlanValidator, SearchQueryPlanValidator>();
            services.AddScoped<ISearchQueryAgent , SearchQueryAgent>();

            services.AddScoped<ISearchIndexWriter,AzureSearchIndexWriter>();
            services.AddScoped<ISearchIndexReader,AzureSearchIndexReader>();

            services.AddScoped<IIndexingUnitOfWork , IndexingUnitOfWork>();
            services.AddScoped<IIndexingRepository, IndexingRepository>();
            services.AddScoped<IIndexingOutboxMessage, IndexingOutboxMessagesRepository>();
            services.AddScoped<IIndexingExecutionRepository, IndexingExecutionRepository>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IAzureSearchIndexInitializer, AzureSearchIndexInitializer>();

            return services;
        }
    }
}
