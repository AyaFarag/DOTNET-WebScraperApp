namespace Shared.Contracts.Agents
{
    public interface IAgentExecutionLogRepository<TSource>
    {
        Task AddAsync(TSource log, CancellationToken cancellationToken);
    }
}
