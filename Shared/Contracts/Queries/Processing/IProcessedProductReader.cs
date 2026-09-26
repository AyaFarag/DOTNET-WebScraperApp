namespace Shared.Contracts.Queries.Processing
{
    public interface IProcessedProductReader<TSource>
    {
        Task<IReadOnlyList<TSource>> GetByBatchIdAsync(Guid batchId,CancellationToken cancellationToken);
    }
}
