using Analytics.Application.Interfaces.AI.AnomalyExplanation;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Repository
{
    public sealed class AnomalyExplanationRepository
    : IAnomalyExplanationRepository
    {
        private readonly AnalyticsDbContext _context;

        public AnomalyExplanationRepository(AnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<AnomalyExplanation?> GetByAnomalyIdAsync(Guid anomalyId,CancellationToken cancellationToken)
        {
            return await _context.AnomalyExplanations.AsNoTracking().FirstOrDefaultAsync(x => x.AnomalyId == anomalyId,cancellationToken);
        }

        public async Task AddAsync(AnomalyExplanation explanation,CancellationToken cancellationToken)
        {
            await _context.AnomalyExplanations.AddAsync(explanation,cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
