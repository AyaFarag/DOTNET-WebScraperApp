using Processing.Domain.Entities;

namespace Processing.Application.Interfaces.Repositories
{
    public interface IProcessedPriceRepository
    {
        Task AddRangeAsync(IEnumerable<ProcessedProduct> prices, CancellationToken cancellationToken);
    }
}
