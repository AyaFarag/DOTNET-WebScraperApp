using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Events.Validation;
using Shared.Contracts.Queries.Validation;
using Validation.Infrastructure.Presistance.Data;

namespace Validation.Infrastructure.Presistance.Repositories
{
    public sealed class ValidatedPriceQuery : IValidatedPriceQuery
    {
        private readonly ValidationDbContext _context;

        public ValidatedPriceQuery(ValidationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ValidatedPriceData>> GetValidPricesAsync(Guid batchId,
                CancellationToken cancellationToken)
        {
            // add join query to get the product name from the RawPrice table
            return await _context.ValidationResults
                .AsNoTracking()
                .Where(x => x.BatchId == batchId)
                .Where(x => x.IsValid)
                .Select(x => new ValidatedPriceData
                {
                    RawPriceId = x.RawPriceId,
                    BatchId = x.BatchId,
                    //ProductName = x.ProductName,
                    //Price = x.Price,
                    //Currency = x.Currency,
                    //Source = x.Source
                })
                .ToListAsync(cancellationToken);
        }
    }
}
