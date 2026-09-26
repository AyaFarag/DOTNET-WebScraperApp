using Classification.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Queries.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Infrastructure.Repositories
{
    public class SearchTaxonomyReader : ISearchTaxonomyReader
    {
        private readonly ClassificationDbContext _context;
        public SearchTaxonomyReader(ClassificationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CategoryExistsAsync(string categoryCode, CancellationToken cancellationToken)
        {
            return await _context.ClassificationResults.AnyAsync(x=>x.CategoryCode == categoryCode, cancellationToken);
        }

        public async Task<bool> ProductTypeExistsAsync(string productTypeCode, CancellationToken cancellationToken)
        {
            return await _context.ClassificationResults.AnyAsync(x => x.ProductTypeCode == productTypeCode, cancellationToken);
        }

        public async Task<bool> SubCategoryExistsAsync(string subCategoryCode, CancellationToken cancellationToken)
        {
            return await _context.ClassificationResults.AnyAsync(x=>x.SubCategoryCode == subCategoryCode, cancellationToken);
        }
    }
}
