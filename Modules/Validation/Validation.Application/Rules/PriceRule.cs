using Shared.Domain;
using Validation.Application.DTOs;
using Validation.Application.Interfaces.Services;

namespace Validation.Application.Rules
{
    using System.Globalization;

    public sealed class PriceRule : IValidationRule
    {
        public string Name => "Price";

        public Task<ValidationRuleResult> ValidateAsync(RawPrice price,CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(price.RawPriceValue))
            {
                return Task.FromResult(ValidationRuleResult.Failure("Price is empty."));
            }

            var normalized = price.RawPriceValue.Replace("AED", "", StringComparison.OrdinalIgnoreCase).Trim();

            if (!decimal.TryParse(
                    normalized,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var value))
            {
                return Task.FromResult(ValidationRuleResult.Failure($"Invalid price: {price.RawPriceValue}"));
            }

            if (value < 0)
            {
                return Task.FromResult(ValidationRuleResult.Failure("Price cannot be negative."));
            }

            return Task.FromResult(ValidationRuleResult.Success());
        }
    }

}
