using Classification.Domain.Enttities;

namespace Classification.Application.Interfaces.Repository
{
    public interface IClassificationExecutionRepository
    {
        Task AddAsync(ClassificationExecution execution, CancellationToken cancellationToken);
    }
}
