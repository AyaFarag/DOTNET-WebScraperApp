using Shared.Contracts.Events.Validation;

namespace Shared.Contracts.Queries.Validation
{
    public interface IValidatedPriceQuery<Tsource>
    {
        Task<IReadOnlyList<Tsource>> GetValidPricesAsync(Guid batchId,CancellationToken cancellationToken);
    }
}
