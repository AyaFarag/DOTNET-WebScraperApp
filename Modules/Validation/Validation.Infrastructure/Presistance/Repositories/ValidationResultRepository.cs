using Validation.Application.Interfaces.Repository;
using Validation.Domain.Entities;
using Validation.Infrastructure.Presistance.Data;

namespace Validation.Infrastructure.Presistance.Repositories
{
    public sealed class ValidationResultRepository
    : IValidationResultRepository
    {
        private readonly ValidationDbContext _context;

        public ValidationResultRepository(
            ValidationDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(
            IEnumerable<ValidationResult> results,
            CancellationToken cancellationToken)
        {
            await _context.ValidationResults.AddRangeAsync(
                results,
                cancellationToken);
        }

    }
}
