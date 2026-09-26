using Classification.Domain.Enttities;

namespace Classification.Application.Interfaces.Repository
{
    public interface ITaxonomyRepository
    {
        Task<IReadOnlyList<ClassificationCategory>> GetActiveCategoriesAsync(CancellationToken cancellationToken);

        Task<ClassificationCategory?> GetByCodeAsync(string code, CancellationToken cancellationToken);

        Task<IReadOnlyList<ClassificationCategory>> GetChildrenAsync(Guid parentId,CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code,CancellationToken cancellationToken);
    }
}
