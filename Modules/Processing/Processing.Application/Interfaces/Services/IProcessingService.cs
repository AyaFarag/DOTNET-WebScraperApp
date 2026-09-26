using Processing.Application.DTOs;
using Shared.Domain;

namespace Processing.Application.Interfaces.Services
{
    public interface IProcessingService
    {
        Task<ProcessingBatchResult> ProcessAsync(Guid batchId, IReadOnlyCollection<RawPrice> rawPrices,
            CancellationToken cancellationToken);
    }
}
