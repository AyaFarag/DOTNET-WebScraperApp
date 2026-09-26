using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Repository;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Repository
{
    public class PriceSnapshotReader : IPriceSnapshotSearchReader
    {
        public Task<AnalyticsSearchResult> SearchAsync(PriceSnapshotSearchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
