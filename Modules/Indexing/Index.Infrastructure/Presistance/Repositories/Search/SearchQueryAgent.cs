using Index.Application.DTOs.Search;
using Index.Application.Interfaces.Search;
using Microsoft.Extensions.AI;

namespace Index.Infrastructure.Presistance.Repositories.Search
{
    public sealed class SearchQueryAgent : ISearchQueryAgent
    {
        private readonly IChatClient _chatClient;

        public SearchQueryAgent(
            IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<SearchQueryPlan> ExecuteQueryPlanAsync(string userQuery,CancellationToken cancellationToken)
        {
            var prompt = $"""
            You are a product search query planner.

            Convert the user's natural language request
            into a structured search query.

            Do NOT execute the search.

            Extract only:
            - search text
            - category
            - subcategory
            - product type
            - brand
            - minimum price
            - maximum price
            - currency
            - source
            - sorting
            - pagination

            User request:
            {userQuery}
            """;

            var response =  await _chatClient.GetResponseAsync<SearchQueryPlan>(prompt,cancellationToken: cancellationToken);
            return response.Result;
        }

        public async Task<SearchQueryPlan> CreateQueryPlanAsync(string userQuery,CancellationToken cancellationToken)
        {
            try
            {
                // AI call
                var result = await ExecuteQueryPlanAsync(userQuery,cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
   
                throw;
            }
        }



    }
}
