using Validation.Domain.Entities;

namespace Validation.Application.Interfaces.Repository
{
    public interface IValidationResultRepository
    {
        Task AddRangeAsync(
            IEnumerable<ValidationResult> results,
            CancellationToken cancellationToken);
    }
}
