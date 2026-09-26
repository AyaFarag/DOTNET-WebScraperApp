using Classification.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Application.Interfaces.Service
{
    public interface IClassificationService
    {
       // Task<ProductClassificationResult> ClassifyAsync(ProcessedProductInput product, CancellationToken cancellationToken);
        Task ClassifyBatchAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
