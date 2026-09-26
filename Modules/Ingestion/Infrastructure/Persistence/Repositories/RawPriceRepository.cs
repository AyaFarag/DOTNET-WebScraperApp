using Application.Interfaces.Repositories;
using Shared.Domain;
using Shared.Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class RawPriceRepository : IRawPriceRepository
    {
        private readonly SharedDbContext _context;

        public RawPriceRepository(
            SharedDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<RawPrice> prices, CancellationToken cancellationToken)
        {
            await _context.RawPrices.AddRangeAsync(prices,cancellationToken);
        }
    }
}
