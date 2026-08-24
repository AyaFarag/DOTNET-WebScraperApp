using Microsoft.Extensions.DependencyInjection;
using Processing.Application.Interfaces.Services;
using Processing.Application.Pipeline;
using Processing.Application.Services;

namespace Processing.Application.Configuration
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProcessingStep, CleaningStep>();
            services.AddScoped<IProcessingStep, ProductNameNormalizationStep>();
            services.AddScoped<IProcessingStep, BrandNormalizationStep>();
            services.AddScoped<IProcessingStep, PriceNormalizationStep>();
            services.AddScoped<IProcessingStep, CurrencyNormalizationStep>();
            services.AddScoped<IProcessingStep, QuantityNormalizationStep>();
            services.AddScoped<IProcessingStep, UnitNormalizationStep>();
            services.AddScoped<IProcessingStep, UnitPriceCalculationStep>();
            services.AddScoped<IProcessingStep, PackageNormalizationStep>();
            services.AddScoped<IProcessingStep, ProductKeyGenerationStep>();


            services.AddScoped<IProcessingService, ProcessingService>();

            // apply automapper configuration for RawPriceProfile
            
            services.AddAutoMapper(cfg => { }, typeof(DependancyInjection).Assembly);


            return services;
        }
    }
}
