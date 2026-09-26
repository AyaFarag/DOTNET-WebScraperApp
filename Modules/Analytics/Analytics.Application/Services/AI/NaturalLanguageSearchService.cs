using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI;
using Analytics.Application.Interfaces.AI.Agent;
using Analytics.Application.Interfaces.AI.Service;
using Analytics.Application.Interfaces.AI.Validator;

namespace Analytics.Application.Services.AI
{
    public sealed class NaturalLanguageSearchService : INaturalLanguageSearchService
    {
        private readonly ISearchQueryAgent _agent;
        private readonly ISearchQueryValidator _validator;
        private readonly IProductSearchReader _searchIndexReader;

        public NaturalLanguageSearchService(
            ISearchQueryAgent agent,
            ISearchQueryValidator validator,
            IProductSearchReader searchIndexReader)
        {
            _agent = agent;
            _validator = validator;
            _searchIndexReader = searchIndexReader;
        }

        public async Task<ProductSearchResult> SearchAsync(string userQuery, CancellationToken cancellationToken)
        {
            var plan = await _agent.CreatePlanAsync(userQuery,cancellationToken);

            var request = _validator.Validate(plan);

            return await _searchIndexReader.SearchAsync(request, cancellationToken);
        }
    }
}
