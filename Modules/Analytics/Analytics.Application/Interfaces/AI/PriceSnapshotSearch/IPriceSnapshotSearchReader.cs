using Analytics.Application.DTOs;
using Analytics.Application.DTOs.AI.Search;

namespace Analytics.Application.Interfaces.AI.PriceSnapshotSearch
{
    public interface IPriceSnapshotSearchReader
    {
        Task<PagedResult<PriceSnapshotSearchResult>> SearchAsync(PriceSnapshotSearchRequest request, CancellationToken cancellationToken);
    }
}
