namespace Analytics.Application.Interfaces.AI.PriceSnapshotSearch
{
    public interface IPriceSnapshotSearchRebuildService
    {
        Task RebuildAsync(
            CancellationToken cancellationToken);
    }
}
