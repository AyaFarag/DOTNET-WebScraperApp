using Analytics.Application.DTOs.AI.AnomalyExplanation;

namespace Analytics.Application.Interfaces.AI.AnomalyExplanation
{
    public interface IAnomalyExplanationAgent
    {
        Task<AnomalyExplanationResult> ExplainAsync(AnomalyExplanationInput input, CancellationToken cancellationToken);
    }
}
