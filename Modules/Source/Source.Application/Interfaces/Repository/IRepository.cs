using Source.Domain.Entities;

namespace Source.Application.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(Guid id ,T entity);
        Task DeleteAsync(T entity);
    }
}
