using Ingestion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBatchRepository
    {
        Task AddAsync(IngestionBatch ingestionBatch, CancellationToken cancellationToken);
    }
}
