using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Source.Application.Interfaces.Repository;
using Source.Domain.Entities;
using Source.Infrastructure.Presistance.Data;

namespace Source.Infrastructure.Presistance.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly SourceDbContext _context;
        public IMapper _mapper;
        protected readonly DbSet<T> _dbSet;

        public Repository(SourceDbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            if (existingEntity == null)
                throw new ArgumentException("Entity not found");

          

            _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id && !e.IsDeleted);

            if (existingEntity == null)
                throw new ArgumentException("Entity not found");

            existingEntity.SoftDelete();

        }
    }
}
