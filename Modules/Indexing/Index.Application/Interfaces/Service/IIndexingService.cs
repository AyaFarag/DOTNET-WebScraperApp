using Index.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.Interfaces.Service
{
    public interface IIndexingService
    {
        Task<IndexingResultDto> IndexBatchAsync(Guid batchId,CancellationToken cancellationToken);
    }
}
