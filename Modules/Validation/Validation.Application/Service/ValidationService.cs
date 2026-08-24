using Shared.Domain;
using Validation.Application.DTOs;
using Validation.Application.Interfaces.Services;

namespace Validation.Application.Service
{
    public sealed class ValidationService : IValidationService
    {
        private readonly IEnumerable<IValidationRule> _rules;

        public ValidationService(IEnumerable<IValidationRule> rules)
        {
            _rules = rules;
        }

        public async Task<ValidationBatchResult> ValidateAsync(Guid batchId,IReadOnlyCollection<RawPrice> rawPrices,
            CancellationToken cancellationToken = default)
        {
            var results = new List<Domain.Entities.ValidationResult>();

            foreach (var price in rawPrices)
            {
                var errors = new List<Domain.Entities.ValidationError>();

                foreach (var rule in _rules)
                {
                    var result = await rule.ValidateAsync(price,cancellationToken);

                    if (!result.IsValid)
                    {
                        errors.Add(new Domain.Entities.ValidationError(rule.Name,result.ErrorMessage!));
                    }
                }

                results.Add(new Domain.Entities.ValidationResult(batchId,price.Id,errors.Count == 0,errors));
            }

            return new ValidationBatchResult(batchId,results);
        }
    }
}
