
using Classification.Application.Interfaces;
using Classification.Application.Interfaces.Agents;
using Classification.Application.Interfaces.Repository;
using Classification.Infrastructure.AI.Agents;
using Classification.Infrastructure.AI.Plugins;
using Classification.Infrastructure.AI.Repository;
using Classification.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Shared.Contracts.Agents;
using Shared.Contracts.Queries.Classification;
using Shared.Contracts.Queries.Indexing;

namespace Classification.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddClassificationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AIOptions>(configuration.GetSection(AIOptions.SectionName));

            var aiOptions = configuration.GetSection(AIOptions.SectionName).Get<AIOptions>()
                ?? throw new InvalidOperationException(
                    "AI configuration is missing.");
            // IAgentExecutionLogRepository
            services.AddScoped<ISearchTaxonomyReader, SearchTaxonomyReader>();
            services.AddScoped<TaxonomyPlugin>();
            services.AddScoped<ITaxonomyRepository,TaxonomyRepository>();
            services.AddScoped<IProductClassificationAgent, ProductClassificationAgent>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IProductClassificationAgent,ProductClassificationAgent>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddScoped<IClassificationExecutionRepository, ClassificationExecutionRepository>();

            services.AddScoped(typeof(IAgentExecutionLogRepository<>), typeof(AgentExecutionLogRepository<>));

            services.AddScoped<IAgentExecutionLogger, AgentExecutionLogger>();
            
            services.AddScoped(typeof(IClassifiedProductReader<>), typeof(ClassifiedProductReader<>));
            services.AddSingleton<Kernel>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                var model = configuration["AI:Model"] ?? throw new InvalidOperationException("AI:Model is missing.");

                var apiKey = configuration["AI:ApiKey"] ?? throw new InvalidOperationException("AI:ApiKey is missing.");

                var builder = Kernel.CreateBuilder();

                builder.AddOpenAIChatCompletion(modelId: model,apiKey: apiKey);

                return builder.Build();
            });


            services.AddSingleton<IChatCompletionService>(sp =>
            {
                var kernel = sp.GetRequiredService<Kernel>();

                return kernel.GetRequiredService<IChatCompletionService>();
            });

            return services;
        }
    }
}
