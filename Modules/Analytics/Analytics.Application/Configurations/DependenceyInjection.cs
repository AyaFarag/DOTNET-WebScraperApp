using Analytics.Application.Interfaces.AI.AnalyticsSearch;
using Analytics.Application.Interfaces.AI.AnomalyExplanation;
using Analytics.Application.Interfaces.AI.SearchQuery;
using Analytics.Application.Interfaces.Anomalies.Service;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Application.Mapper.Execution;
using Analytics.Application.Services.AI;
using Analytics.Application.Services.Anomalies;
using Analytics.Application.Services.PriceAnalytics;
using Analytics.Application.Services.PriceIndices;
using Analytics.Application.Services.Weights;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Application.Configurations
{
    public static class DependenceyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<IPriceAnalyticsService, PriceAnalyticsService>();
            services.AddScoped<IAnomalyDetector, IqrAnomalyDetector>();
            services.AddScoped<IAnomalyDetectionService, AnomalyDetectionService>();
            services.AddScoped<IPriceTrendService, PriceTrendService>();
            services.AddScoped<IMarketAnalysisService, MarketAnalysisService>();
            services.AddScoped<IAnalyticsDashboardService, AnalyticsDashboardService>();
            services.AddAutoMapper(cfs => { }, typeof(ExecutionProfile).Assembly);
            services.AddScoped<IAnomalyExplanationService, AnomalyExplanationService>();
            services.AddScoped<ISearchQueryValidator,SearchQueryValidator>();
            services.AddScoped<INaturalLanguageSearchService,NaturalLanguageSearchService>();
            services.AddScoped<IPriceRelativeService,PriceRelativeService>();
            services.AddScoped<IPriceWeightService,PriceWeightService>();
            services.AddScoped<IWeightedPriceService,WeightedPriceService>();
            services.AddScoped<ICategoryIndexService, CategoryIndexService>();
            services.AddScoped<ILaspeyresIndexService, LaspeyresIndexService>();
            services.AddScoped<IPaascheIndexService, PaascheIndexService>();
            services.AddScoped<IFisherIndexService, FisherIndexService>();
            services.AddScoped<ICpiWeightService, CpiWeightService>();
            services.AddScoped<IConsumerPriceIndexService, ConsumerPriceIndexService>();
            services.AddScoped<IPpiWeightService,PpiWeightService>();
            services.AddScoped<IProducerPriceIndexService,ProducerPriceIndexService>();

            services.AddScoped<IAnalyticsSearchService, AnalyticsSearchService>();

            return services;
        }
    }
}
