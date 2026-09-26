using Index.Application.DTOs.Search;
using Index.Application.Interfaces.Search;
using Shared.Contracts.Agents;
using System.Text.Json;

namespace Index.Application.Service
{
    public sealed class NaturalLanguageSearchService : INaturalLanguageSearchService
    {
        private readonly ISearchQueryAgent _agent;
        private readonly ISearchQueryPlanValidator _validator;
        private readonly ISearchIndexReader _searchReader;
        private readonly IAgentExecutionLogger _agentExecutionLogger;

        public NaturalLanguageSearchService(
            ISearchQueryAgent agent,
            ISearchQueryPlanValidator validator,
            ISearchIndexReader searchReader,
            IAgentExecutionLogger agentExecutionLogger)
        {
            _agent = agent;
            _validator = validator;
            _searchReader = searchReader;
            _agentExecutionLogger = agentExecutionLogger;
        }

        public async Task<SearchResultDto> SearchAsync(string query, CancellationToken cancellationToken)
        {
            // ============================================
            // 1. Execute AI Agent
            // ============================================

            var executionId = Guid.NewGuid();
            var startedAt = DateTime.UtcNow;

            SearchQueryPlan plan;

            try
            {
                plan = await _agent.CreateQueryPlanAsync(query,cancellationToken);

                var completedAt = DateTime.UtcNow;

                await _agentExecutionLogger.LogAsync(new AgentExecutionLogEntry
                    {
                        Id = executionId,

                        AgentName = "SearchQueryAgent",
                        Module = "Indexing",

                        StartedAt = startedAt,
                        CompletedAt = completedAt,
                        LatencyMs =(long)(completedAt - startedAt).TotalMilliseconds,

                        Status = "Success",

                        Input = query,

                        Output = JsonSerializer.Serialize(plan),

                        PromptVersion = "search-query-v1"
                    },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                var completedAt = DateTime.UtcNow;

                await _agentExecutionLogger.LogAsync(new AgentExecutionLogEntry
                    {
                        Id = executionId,

                        AgentName = "SearchQueryAgent",
                        Module = "Indexing",

                        StartedAt = startedAt,
                        CompletedAt = completedAt,

                        LatencyMs = (long)(completedAt - startedAt).TotalMilliseconds,

                        Status = "Failed",

                        Input = query,

                        Error = ex.Message,

                        PromptVersion = "search-query-v1"
                    },
                    cancellationToken);

                throw;
            }


            // ============================================
            // 2. Validate / Normalize AI output
            // ============================================

            plan = await _validator.ValidateAsync(plan,cancellationToken);

            // ============================================
            // 3. Convert AI plan to Search Request
            // ============================================

            var request = new ProductSearchRequest
            {
                Query = plan.SearchText,
                CategoryCode = plan.CategoryCode,
                SubCategoryCode = plan.SubCategoryCode,
                ProductTypeCode = plan.ProductTypeCode,
                Brand = plan.Brand,
                MinPrice = plan.MinPrice,
                MaxPrice = plan.MaxPrice,
                Currency = plan.Currency,
                Source = plan.Source,
                SortBy = plan.SortBy,
                Descending = plan.Descending,
                Page = plan.Page,
                PageSize = plan.PageSize
            };

            // ============================================
            // 4. Execute actual search
            // ============================================

            return await _searchReader.SearchAsync(request,cancellationToken);
        }
    }
}
