using Shared.Domain;

namespace Application.Interfaces.Repositories
{

    public interface IRawPriceRepository
    {
        Task AddRangeAsync(
            IEnumerable<RawPrice> prices,
            CancellationToken cancellationToken);
    }
}
