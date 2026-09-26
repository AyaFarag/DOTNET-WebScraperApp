using Index.Domain.Entities;

namespace Index.Application.Interfaces.Repository
{
    public interface IIndexingExecutionRepository
    {
        Task AddAsync(IndexingExecution execution, CancellationToken cancellationToken);
    }
}
