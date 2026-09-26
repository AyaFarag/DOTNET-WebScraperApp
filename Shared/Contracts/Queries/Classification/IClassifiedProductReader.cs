namespace Shared.Contracts.Queries.Classification
{
    public interface IClassifiedProductReader<TSource>
    {
        Task<List<TSource>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
