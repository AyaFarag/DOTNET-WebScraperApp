namespace Processing.Application.Interfaces.Repositories
{
    public interface IIdempotencyService
    {
        Task<bool> IsProcessedAsync(Guid eventId, CancellationToken cancellationToken);

        Task MarkAsProcessedAsync(Guid eventId, string eventType, CancellationToken cancellationToken);
    }
}
