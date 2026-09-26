namespace Shared.Contracts.Agents
{
    public interface IAgentExecutionLogger
    {
        Task LogAsync(AgentExecutionLogEntry entry,CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<AgentExecutionLogEntry>> GetByAgentAsync(string agentName, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<AgentExecutionLogEntry>> GetByBatchAsync(Guid batchId,CancellationToken cancellationToken = default);
    }
}
