using Index.Application.DTOs;
using Index.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Queries.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Infrastructure.Presistance.Repositories
{
    public class IndexedProductReader<T> : IIndexedProductReader<IndexedProductDto>
    {
        private readonly IndexingDbContext _context;
        public IndexedProductReader(IndexingDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<IndexedProductDto>> GetByBatchIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            return await _context.indexedProducts
                .Where(p => p.BatchId == batchId)
                .Select(p => new IndexedProductDto
                {
                    Id = p.Id,
                    ProductKey = p.ProductKey,
                    ProductName = p.ProductName,
                    Brand = p.Brand,
                    CategoryCode = p.CategoryCode,
                    Source = p.Source,
                    Price = p.Price,
                    UnitPrice = p.UnitPrice,
                    Currency = p.Currency,
                    CollectedAt = p.CollectedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
