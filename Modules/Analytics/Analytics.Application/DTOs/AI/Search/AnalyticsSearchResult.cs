using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record AnalyticsSearchResult(
        PagedResult<PriceSnapshotDto> Items,
        int Page,
        int PageSize,
        long TotalCount)
    {
        public int TotalPages =>
            (int)Math.Ceiling(
                TotalCount / (double)PageSize);
    }
}
