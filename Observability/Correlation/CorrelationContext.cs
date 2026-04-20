using BuildingBlocks.Observability.Abstractions.Correlation;

namespace BuildingBlocks.Observability.Correlation;

public class CorrelationContext : ICorrelationContext
{
    private string _id;

    public string Id => _id;

    public void Set(string id)
    {
        _id = id;
    }
}
