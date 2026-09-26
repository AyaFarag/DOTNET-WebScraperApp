using Classification.Application.DTOs;

namespace Classification.Application.Interfaces.Agents
{
    public interface IProductClassificationAgent
    {
        //Task<ProductClassificationResult> ClassifyAsync(ProcessedProductInput product, CancellationToken cancellationToken);
        Task<AgentExecutionResult<ProductClassificationResult>> ClassifyAsync(ProcessedProductInput product, CancellationToken cancellationToken);
    }
}
