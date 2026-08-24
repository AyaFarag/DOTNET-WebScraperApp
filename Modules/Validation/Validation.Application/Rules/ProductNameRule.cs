using Shared.Domain;
using Validation.Application.DTOs;
using Validation.Application.Interfaces.Services;

namespace Validation.Application.Rules
{
    public sealed class ProductNameRule : IValidationRule
    {
        public string Name => "ProductName";

        public Task<ValidationRuleResult> ValidateAsync(RawPrice price,CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(price.ProductName))
            {
                return Task.FromResult(ValidationRuleResult.Failure("Product name cannot be empty."));
            }

            if (price.ProductName.Length > 500)
            {
                return Task.FromResult(ValidationRuleResult.Failure("Product name cannot exceed 500 characters."));
            }

            return Task.FromResult(ValidationRuleResult.Success());
        }
    }
}
