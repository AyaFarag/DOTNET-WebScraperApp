using Analytics.Application.DTOs.AI;
using Analytics.Application.Interfaces.AI.Agent;
using Analytics.Application.Interfaces.AI.Repository;
using Analytics.Application.Interfaces.AI.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.AI
{
    public sealed class AnomalyExplanationService : IAnomalyExplanationService
    {
        private readonly IPriceAnomalyRepository _anomalyRepository;
        private readonly IAnomalyExplanationRepository _explanationRepository;
        private readonly IAnomalyExplanationAgent _agent;

        public AnomalyExplanationService(
            IPriceAnomalyRepository anomalyRepository,
            IAnomalyExplanationRepository explanationRepository,
            IAnomalyExplanationAgent agent)
        {
            _anomalyRepository = anomalyRepository;
            _explanationRepository = explanationRepository;
            _agent = agent;
        }

        public async Task ExplainAsync(Guid anomalyId,CancellationToken cancellationToken)
        {
            var existing = await _explanationRepository.GetByAnomalyIdAsync(anomalyId,cancellationToken);

            if (existing is not null)
                return;

            var anomaly = await _anomalyRepository.GetByIdAsync(anomalyId,cancellationToken);

            if (anomaly is null)
                return;

            var input = new AnomalyExplanationInput
            {
                ProductKey = anomaly.ProductKey,
                ProductName = anomaly.ProductName,
                Brand = anomaly.Brand,
                CategoryCode = anomaly.CategoryCode,
                Source = anomaly.Source,
                Currency = anomaly.Currency,
                Price = anomaly.Price,
                Median = anomaly.Median,
                Q1 = anomaly.Q1,
                Q3 = anomaly.Q3,
                Iqr = anomaly.Iqr,
                LowerBound = anomaly.LowerBound,
                UpperBound = anomaly.UpperBound,
                DeviationPercentage = anomaly.DeviationPercentage,
                Severity = anomaly.Severity,
                Direction = anomaly.Direction
            };

            var result = await _agent.ExplainAsync(input,cancellationToken);

            var explanation = new AnomalyExplanation(
                anomaly.Id,
                result.Summary,
                result.Explanation,
                result.PossibleInterpretation,
                result.Confidence,
                model: "configured-model",
                promptVersion: "v1");

            await _explanationRepository.AddAsync(explanation, cancellationToken);
        }
    }
}
