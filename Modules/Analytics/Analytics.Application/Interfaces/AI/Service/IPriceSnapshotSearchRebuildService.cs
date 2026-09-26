namespace Analytics.Application.Interfaces.AI.Service
{
    public interface IPriceSnapshotSearchRebuildService
    {
        Task RebuildAsync(
            CancellationToken cancellationToken);
    }
}
