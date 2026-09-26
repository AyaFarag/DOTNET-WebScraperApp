using Classification.Application.DTOs;
using Classification.Application.Interfaces.Repository;

namespace Classification.Infrastructure.Repositories
{
    public sealed class TaxonomyValidator : ITaxonomyValidator
    {
        private readonly ITaxonomyRepository _repository;

        public TaxonomyValidator(ITaxonomyRepository repository)
        {
            _repository = repository;
        }

        public async Task ValidateAsync(AgentExecutionResult<ProductClassificationResult> result, CancellationToken cancellationToken)
        {
            await ValidateCode(result.Result.CategoryCode,cancellationToken);

            await ValidateCode(result.Result.SubCategoryCode,cancellationToken);

            await ValidateCode(result.Result.ProductGroupCode,cancellationToken);

            await ValidateCode(result.Result.ProductTypeCode,cancellationToken);
        }

        private async Task ValidateCode(string code,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new InvalidOperationException(
                    "Classification taxonomy code is empty.");
            }

            var exists = await _repository.ExistsByCodeAsync(code,cancellationToken);

            if (!exists)
            {
                throw new InvalidOperationException(
                    $"Unknown taxonomy code: {code}");
            }
        }
    }
}
