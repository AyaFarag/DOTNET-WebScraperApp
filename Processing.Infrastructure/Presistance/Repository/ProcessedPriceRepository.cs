using Processing.Application.Interfaces.Repositories;
using Processing.Domain.Entities;
using Processing.Infrastructure.Presistance.Data;

namespace Processing.Infrastructure.Presistance.Repository
{
    public sealed class ProcessedPriceRepository : IProcessedPriceRepository
    {
        private readonly ProcessingDbContext _context;

        public ProcessedPriceRepository(ProcessingDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<ProcessedPrice> prices, CancellationToken cancellationToken)
        {
            await _context.ProcessedPrices.AddRangeAsync(prices, cancellationToken);
        }
    }
}
