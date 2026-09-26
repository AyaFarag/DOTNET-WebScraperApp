namespace Shared.Contracts.Queries.Indexing
{
    public interface IIndexedProductReader<Tsource>
    {
        Task<IReadOnlyCollection<Tsource>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
