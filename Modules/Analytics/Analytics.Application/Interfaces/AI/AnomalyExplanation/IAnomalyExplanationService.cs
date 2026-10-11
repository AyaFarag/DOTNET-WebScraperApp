namespace Analytics.Application.Interfaces.AI.AnomalyExplanation
{
    public interface IAnomalyExplanationService
    {
        Task ExplainAsync(
            Guid anomalyId,
            CancellationToken cancellationToken);
    }
}
