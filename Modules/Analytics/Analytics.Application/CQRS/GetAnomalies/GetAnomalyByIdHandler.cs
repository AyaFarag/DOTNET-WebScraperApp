using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using MediatR;

namespace Analytics.Application.CQRS.GetAnomalies
{
    public sealed class GetAnomalyByIdHandler : IRequestHandler<GetAnomalyByIdQuery,PriceAnomalyDto?>
    {
        private readonly IPriceAnomalyRepository _repository;

        public GetAnomalyByIdHandler(IPriceAnomalyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PriceAnomalyDto?> Handle(GetAnomalyByIdQuery request,CancellationToken cancellationToken)
        {
            var anomaly = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (anomaly is null)
                return null;

            return new PriceAnomalyDto
            {
                Id = anomaly.Id,
                SnapshotId = anomaly.SnapshotId,
                ProductKey = anomaly.ProductKey,
                ProductName = anomaly.ProductName,
                Brand = anomaly.Brand,
                CategoryCode = anomaly.CategoryCode,
                Source = anomaly.Source,
                Currency = anomaly.Currency,
                Price = anomaly.Price,
                Median = anomaly.Median,
                LowerBound = anomaly.LowerBound,
                UpperBound = anomaly.UpperBound,
                DeviationPercentage = anomaly.DeviationPercentage,
                Severity = anomaly.Severity,
                Direction = anomaly.Direction,
                DetectedAt = anomaly.DetectedAt
            };
        }
    }
}
