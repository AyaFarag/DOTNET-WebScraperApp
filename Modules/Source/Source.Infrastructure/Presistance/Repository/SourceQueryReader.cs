using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Queries.Source;
using Source.Infrastructure.Presistance.Data;

namespace Source.Infrastructure.Presistance.Repository
{
    public class SourceQueryReader<T> : ISourceQueryReader<Source.Domain.Entities.Source>
    {
        public SourceDbContext _context;
        public SourceQueryReader(SourceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Source.Domain.Entities.Source>> GetAllSourceQueryAsync()
        {
            return await _context.Sources.ToListAsync();
        }


    }
}
