using Shared.Contracts.Events.Validation;

namespace Shared.Contracts.Queries.Validation
{
    public interface IValidatedPriceQuery
    {
        Task<IReadOnlyList<ValidatedPriceData>> GetValidPricesAsync(Guid batchId,CancellationToken cancellationToken);
    }
}
