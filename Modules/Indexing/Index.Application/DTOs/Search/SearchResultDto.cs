using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.DTOs.Search
{
    public sealed class SearchResultDto
    {
        public IReadOnlyCollection<ProductSearchResultDto> Items { get; init; }
            = [];

        public int TotalCount { get; init; }

        public int Page { get; init; }

        public int PageSize { get; init; }

        public int TotalPages =>
            PageSize == 0
                ? 0
                : (int)Math.Ceiling(
                    TotalCount / (double)PageSize);
    }
}
