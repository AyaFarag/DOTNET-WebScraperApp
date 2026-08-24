using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Queries.Ingestion;
using Shared.Domain;

namespace Infrastructure.Persistence.Repositories
{
    public class RawPriceQueryReader : IRawPriceQueryReader
    {
        private readonly IngestionDbContext _context;
        public RawPriceQueryReader(IngestionDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsAsync(Guid batchId, string productName, string source, CancellationToken cancellationToken)
        {
            return await _context.RawPrices.AnyAsync(rp =>
                rp.BatchId == batchId &&
                rp.ProductName == productName &&
                rp.Source == source, cancellationToken);
        }

        public async Task<IReadOnlyCollection<RawPrice>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var rawPrices = await _context.RawPrices.Where(rp => rp.BatchId == batchId).ToListAsync(cancellationToken);
            return (IReadOnlyCollection<RawPrice>)rawPrices;
        }
    }
}       
