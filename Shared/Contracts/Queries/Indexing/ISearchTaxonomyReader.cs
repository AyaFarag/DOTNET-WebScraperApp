using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Queries.Indexing
{
    public interface ISearchTaxonomyReader
    {
        Task<bool> CategoryExistsAsync(
            string categoryCode,
            CancellationToken cancellationToken);

        Task<bool> SubCategoryExistsAsync(
            string subCategoryCode,
            CancellationToken cancellationToken);

        Task<bool> ProductTypeExistsAsync(
            string productTypeCode,
            CancellationToken cancellationToken);
    }
}
