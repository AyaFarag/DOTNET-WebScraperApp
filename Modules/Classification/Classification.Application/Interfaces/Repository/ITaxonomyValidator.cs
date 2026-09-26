using Classification.Application.DTOs;

namespace Classification.Application.Interfaces.Repository
{
    public interface ITaxonomyValidator
    {
        Task ValidateAsync(AgentExecutionResult<ProductClassificationResult> result, CancellationToken cancellationToken);
    }
}
