using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Data;
using Ingestion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public sealed class IngestionExecutionRepository
     : IIngestionExecutionRepository
    {
        private readonly IngestionDbContext _context;

        public IngestionExecutionRepository(
            IngestionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            IngestionExecution execution,
            CancellationToken cancellationToken)
        {
            await _context.IngestionExecutions.AddAsync(
                execution,
                cancellationToken);
        }

        public async Task<IngestionExecution?> GetByBatchIdAsync(
            Guid batchId,
            CancellationToken cancellationToken)
        {
            return await _context.IngestionExecutions
                .FirstOrDefaultAsync(
                    x => x.BatchId == batchId,
                    cancellationToken);
        }

        public Task UpdateAsync(
            IngestionExecution execution,
            CancellationToken cancellationToken)
        {
            _context.IngestionExecutions.Update(execution);

            return Task.CompletedTask;
        }
    }
}
