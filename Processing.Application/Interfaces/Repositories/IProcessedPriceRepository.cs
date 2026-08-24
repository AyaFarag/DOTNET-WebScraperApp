using Processing.Domain.Entities;

namespace Processing.Application.Interfaces.Repositories
{
    public interface IProcessedPriceRepository
    {
        Task AddRangeAsync(IEnumerable<ProcessedPrice> prices, CancellationToken cancellationToken);
    }
}
