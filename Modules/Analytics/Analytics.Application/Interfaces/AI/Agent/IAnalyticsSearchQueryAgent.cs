using Analytics.Application.DTOs.AI.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Interfaces.AI.Agent
{
    public interface IAnalyticsSearchQueryAgent
    {
        Task<AnalyticsSearchQueryPlan> CreatePlanAsync(
            string query,
            CancellationToken cancellationToken);
    }
}
