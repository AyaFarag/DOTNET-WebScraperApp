using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces.AI.AnomalyExplanation
{
    public interface IAnomalyExplanationRepository
    {
        Task<Domain.Entities.AnomalyExplanation?> GetByAnomalyIdAsync(Guid anomalyId, CancellationToken cancellationToken);

        Task AddAsync(Domain.Entities.AnomalyExplanation explanation,CancellationToken cancellationToken);
    }
}
