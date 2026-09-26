namespace BuildingBlocks.Observability.Abstractions.Correlation;

public interface ICorrelationContext
{
    string Id { get; }
    void Set(string id);
}
