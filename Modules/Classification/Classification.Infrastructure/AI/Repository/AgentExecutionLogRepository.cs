using Classification.Infrastructure.Persistence.Data;
using Shared.Contracts.Agents;
using AgentExecutionLog = Classification.Domain.Enttities.AgentExecutionLog;

namespace Classification.Infrastructure.AI.Repository
{
    public sealed class AgentExecutionLogRepository<T> : IAgentExecutionLogRepository<AgentExecutionLog>
    {
        private readonly ClassificationDbContext _context;

        public AgentExecutionLogRepository(ClassificationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AgentExecutionLog log, CancellationToken cancellationToken)
        {
            await _context.AgentExecutionLogs.AddAsync(log, cancellationToken);
        }
    }
}
