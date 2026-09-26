using Analytics.Application.Interfaces;
using Analytics.Application.Interfaces.AI.Service;
using Analytics.Application.Interfaces.AI.Validator;
using Analytics.Application.Mapper.Execution;
using Analytics.Application.Services;
using Analytics.Application.Services.AI;
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

            return services;
        }
    }
}
