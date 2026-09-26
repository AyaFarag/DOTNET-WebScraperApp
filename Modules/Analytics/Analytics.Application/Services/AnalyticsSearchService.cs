using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Agent;
using Analytics.Application.Interfaces.AI.Logic;
using Analytics.Application.Interfaces.AI.Repository;
using Analytics.Application.Interfaces.AI.Validator;

namespace Analytics.Application.Services
{
    public sealed class AnalyticsSearchService
        : IAnalyticsSearchService
    {
        private readonly IAnalyticsSearchQueryAgent _agent;
        private readonly IAnalyticsSearchQueryValidator _validator;
        private readonly IPriceSnapshotSearchRequestMapper _mapper;
        private readonly IPriceSnapshotSearchReader _reader;

        public AnalyticsSearchService(
            IAnalyticsSearchQueryAgent agent,
            IAnalyticsSearchQueryValidator validator,
            IPriceSnapshotSearchRequestMapper mapper,
            IPriceSnapshotSearchReader reader)
        {
            _agent = agent;
            _validator = validator;
            _mapper = mapper;
            _reader = reader;
        }

        public async Task<AnalyticsSearchResult> SearchAsync(
            string query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException(
                    "Search query cannot be empty.",
                    nameof(query));
            }

            var plan =
                await _agent.CreatePlanAsync(
                    query,
                    cancellationToken);

            _validator.Validate(plan);

            var request =
                _mapper.Map(
                    plan,
                    DateTime.UtcNow);

            return await _reader.SearchAsync(
                request,
                cancellationToken);
        }
    }
}
