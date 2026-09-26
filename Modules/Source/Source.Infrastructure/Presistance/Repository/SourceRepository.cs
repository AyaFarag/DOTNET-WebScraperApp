using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Source.Application.Interfaces.Repository;
using Source.Infrastructure.Presistance.Data;

namespace Source.Infrastructure.Presistance.Repository
{
    public class SourceRepository : Repository<Domain.Entities.Source>, ISourceRepository
    {
        public readonly SourceDbContext _context;
        public SourceRepository(SourceDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        

        public async Task<bool> DisableSource(Guid id)
        {
            try
            {
              return  await _context.Sources
                .Where(s => s.Id == id && s.IsDeleted == false)
                .ExecuteUpdateAsync(s => s.SetProperty(source => source.IsEnabled, false)) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while disabling the source with ID {id}: {ex.Message}", ex);
            }
                 
        }

        public async Task<bool> EnableSource(Guid id)
        { 
            try
            {
                 return await _context.Sources
                .Where(s => s.Id == id && s.IsDeleted == false)
                .ExecuteUpdateAsync(s => s.SetProperty(source => source.IsEnabled, true)) > 0;
             
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while enabling the source with ID {id}: {ex.Message}", ex);
            }

        }
    }
}
