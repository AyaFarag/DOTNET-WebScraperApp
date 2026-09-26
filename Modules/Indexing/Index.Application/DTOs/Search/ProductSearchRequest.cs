using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.DTOs.Search
{
    public sealed class ProductSearchRequest
    {
        public string? Query { get; init; }

        public string? CategoryCode { get; init; }

        public string? SubCategoryCode { get; init; }
        public string? ProductTypeCode { get; init; }

        public string? Brand { get; init; }

        public string? Source { get; init; }

        public decimal? MinPrice { get; init; }

        public decimal? MaxPrice { get; init; }

        public string? Currency { get; init; }

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;

        public string? SortBy { get; init; }

        public bool Descending { get; init; }
    }
}
