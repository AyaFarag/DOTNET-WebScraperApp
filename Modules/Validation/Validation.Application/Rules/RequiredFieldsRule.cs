using Shared.Domain;
using Validation.Application.DTOs;
using Validation.Application.Interfaces.Services;

namespace Validation.Application.Rules
{
    public sealed class RequiredFieldsRule : IValidationRule
    {
        public string Name => "RequiredFields";

        public Task<ValidationRuleResult> ValidateAsync(RawPrice price,CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(price.ProductName))
            {
                return Task.FromResult(ValidationRuleResult.Failure("Product name is required."));
            }

            if (string.IsNullOrWhiteSpace(price.RawPriceValue))
            {
                return Task.FromResult(ValidationRuleResult.Failure("Price is required."));
            }

            if (string.IsNullOrWhiteSpace(price.Source))
            {
                return Task.FromResult( ValidationRuleResult.Failure( "Source is required."));
            }

            return Task.FromResult(ValidationRuleResult.Success());
        }
    }
}
