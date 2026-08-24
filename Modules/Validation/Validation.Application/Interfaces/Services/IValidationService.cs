using Shared.Domain;
using Validation.Application.DTOs;

namespace Validation.Application.Interfaces.Services
{

    public interface IValidationService
    {
        Task<ValidationBatchResult> ValidateAsync(Guid batchId,IReadOnlyCollection<RawPrice> rawPrices,CancellationToken cancellationToken = default);
    }
    
}
