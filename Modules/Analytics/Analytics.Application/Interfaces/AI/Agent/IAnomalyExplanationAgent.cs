using Analytics.Application.DTOs.AI.AnomalyExplanation;

namespace Analytics.Application.Interfaces.AI.Agent
{
    public interface IAnomalyExplanationAgent
    {
        Task<AnomalyExplanationResult> ExplainAsync(AnomalyExplanationInput input, CancellationToken cancellationToken);
    }
}
