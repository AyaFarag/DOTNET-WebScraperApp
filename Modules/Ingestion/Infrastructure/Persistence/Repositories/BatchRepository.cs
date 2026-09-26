using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Data;
using Ingestion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly IngestionDbContext _context;
        public BatchRepository(IngestionDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(IngestionBatch ingestionBatch, CancellationToken cancellationToken)
        {
            await _context.IngestionBatches.AddAsync(ingestionBatch, cancellationToken);
        }
    }
}
