using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Queries.Validation;
using Validation.Application.Interfaces;
using Validation.Application.Interfaces.Repository;
using Validation.Application.Interfaces.Services;
using Validation.Application.Rules;
using Validation.Application.Service;
using Validation.Infrastructure.Presistance.Data;
using Validation.Infrastructure.Presistance.Repositories;

namespace Validation.Infrastructure.Configurations
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your infrastructure services here
            // For example:
            services.AddDbContext<ValidationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IIdempotencyService,IdempotencyService>();
            services.AddScoped<IUnitOfWork, ValidationUnitOfWork>();
            services.AddScoped<IValidationService, ValidationService>();

            services.AddScoped<IValidationRule, RequiredFieldsRule>();
            services.AddScoped<IValidationRule, ProductNameRule>();
            services.AddScoped<IValidationRule, PriceRule>();
            services.AddScoped<IValidationRule, CurrencyRule>();
            services.AddScoped<IValidationRule, DuplicatePriceRule>();

            
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IValidationResultRepository, ValidationResultRepository>();
            services.AddScoped<IValidatedPriceQuery, ValidatedPriceQuery>();




            return services;
        }
    }
}
