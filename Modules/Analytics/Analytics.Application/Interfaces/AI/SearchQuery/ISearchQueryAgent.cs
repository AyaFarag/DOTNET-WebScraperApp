using Analytics.Application.DTOs.AI.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Interfaces.AI.SearchQuery
{
    public interface ISearchQueryAgent
    {
        Task<SearchQueryPlan> CreatePlanAsync(
            string userQuery,
            CancellationToken cancellationToken);
    }
}
