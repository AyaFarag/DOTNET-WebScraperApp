using AutoMapper;
using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Agents;

namespace Classification.Infrastructure.AI.Repository
{
    public class AgentExecutionLogger : IAgentExecutionLogger
    {
        private IMapper _mapper;
        private readonly ClassificationDbContext _context;
        public AgentExecutionLogger(ClassificationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // an endpint for UI app to get all logs for a given agent name
        public async Task<IReadOnlyCollection<AgentExecutionLogEntry>> GetByAgentAsync(string agentName, CancellationToken cancellationToken = default)
        {
            return await _context.AgentExecutionLogs.Where(log => log.AgentName == agentName)
                .Select(log => new AgentExecutionLogEntry
                {
                    Id = log.Id,
                    AgentName = log.AgentName,
                    Module = log.Module,
                    BatchId = log.BatchId,
                    EntityId = log.EntityId,
                    Model = log.Model,
                    PromptVersion = log.PromptVersion,
                    StartedAt = log.StartedAt,
                    CompletedAt = log.CompletedAt,
                    LatencyMs = log.LatencyMs,
                    InputTokens = log.InputTokens,
                    OutputTokens = log.OutputTokens,
                    TotalTokens = log.TotalTokens,
                    Confidence = log.Confidence,
                    Status = log.Status,
                    Error = log.Error,
                    Input = log.Input,
                    Output = log.Output
                })
                .ToListAsync(cancellationToken);
        }


        // an endpint for UI app to get all logs for a given batch id
        public async Task<IReadOnlyCollection<AgentExecutionLogEntry>> GetByBatchAsync(Guid batchId, CancellationToken cancellationToken = default)
        {
            return await _context.AgentExecutionLogs
                .Where(log => log.BatchId == batchId)
                .Select(log => new AgentExecutionLogEntry
                {
                    Id = log.Id,
                    AgentName = log.AgentName,
                    Module = log.Module,
                    BatchId = log.BatchId,
                    EntityId = log.EntityId,
                    Model = log.Model,
                    PromptVersion = log.PromptVersion,
                    StartedAt = log.StartedAt,
                    CompletedAt = log.CompletedAt,
                    LatencyMs = log.LatencyMs,
                    InputTokens = log.InputTokens,
                    OutputTokens = log.OutputTokens,
                    TotalTokens = log.TotalTokens,
                    Confidence = log.Confidence,
                    Status = log.Status,
                    Error = log.Error,
                    Input = log.Input,
                    Output = log.Output
                })
                .ToListAsync(cancellationToken);
        }


        // add new agent log entry to the database
        // but different than the create agent log in form the main entity
        // use when you want to send a dto rather than and entity
        public async Task LogAsync(AgentExecutionLogEntry entry, CancellationToken cancellationToken = default)
        {
            var logEntity = _mapper.Map<Domain.Enttities.AgentExecutionLog>(entry);
            await _context.AgentExecutionLogs.AddAsync(logEntity, cancellationToken);
        }

    }
}
