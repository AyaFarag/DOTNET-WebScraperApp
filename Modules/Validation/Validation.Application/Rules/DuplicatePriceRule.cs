using Shared.Contracts.Queries.Ingestion;
using Shared.Domain;
using Validation.Application.DTOs;
using Validation.Application.Interfaces.Repository;
using Validation.Application.Interfaces.Services;

namespace Validation.Application.Rules
{
    public sealed class DuplicatePriceRule : IValidationRule
    {
        private readonly IRawPriceQueryReader _repository;

        public string Name => "DuplicatePrice";

        public DuplicatePriceRule(IRawPriceQueryReader repository)
        {
            _repository = repository;
        }

        public async Task<ValidationRuleResult> ValidateAsync(RawPrice price,CancellationToken cancellationToken = default)
        {
            var exists = await _repository.ExistsAsync(
                price.BatchId,
                price.ProductName,
                price.Source,
                cancellationToken);

            if (exists)
            {
                return ValidationRuleResult.Failure("Duplicate price record.");
            }

            return ValidationRuleResult.Success();
        }
    }
}
