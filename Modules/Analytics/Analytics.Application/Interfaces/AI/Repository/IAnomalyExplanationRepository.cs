using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.AI.Repository
{
    public interface IAnomalyExplanationRepository
    {
        Task<AnomalyExplanation?> GetByAnomalyIdAsync(Guid anomalyId, CancellationToken cancellationToken);

        Task AddAsync(AnomalyExplanation explanation,CancellationToken cancellationToken);
    }
}
