using Classification.Application.DTOs;
using Classification.Domain.Enttities;

namespace Classification.Application.Interfaces.Agents
{
    public interface IClassificationAgent
    {
        Task<ProductClassificationResult> ClassifyAsync(ProcessedProductInput product, CancellationToken cancellationToken);
    }
}
