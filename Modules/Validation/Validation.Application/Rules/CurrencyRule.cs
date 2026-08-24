using Shared.Domain;
using Validation.Application.DTOs;
using Validation.Application.Interfaces.Services;

namespace Validation.Application.Rules
{
    public sealed class CurrencyRule : IValidationRule
    {
        public string Name => "Currency";

        public Task<ValidationRuleResult> ValidateAsync(RawPrice price,CancellationToken cancellationToken = default)
        {
            if (!price.RawPriceValue.Contains("AED",StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(ValidationRuleResult.Failure( "Price must be in AED."));
            }

            return Task.FromResult(ValidationRuleResult.Success());
        }
    }
}
