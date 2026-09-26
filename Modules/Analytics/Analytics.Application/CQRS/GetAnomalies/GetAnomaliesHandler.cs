using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.Repositories;
using MediatR;

namespace Analytics.Application.CQRS.GetAnomalies
{
    public sealed class GetAnomaliesHandler : IRequestHandler<GetAnomaliesQuery, PagedResult<PriceAnomalyDto>>
    {
        private readonly IPriceAnomalyRepository _repository;

        public GetAnomaliesHandler(IPriceAnomalyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<PriceAnomalyDto>> Handle(GetAnomaliesQuery request,CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(
                request.ProductKey,
                request.CategoryCode,
                request.Source,
                request.Severity,
                request.Direction,
                request.From,
                request.To,
                request.Page,
                request.PageSize,
                cancellationToken);

            var items = result.Items
                .Select(x => new PriceAnomalyDto
                {
                    Id = x.Id,
                    SnapshotId = x.SnapshotId,
                    ProductKey = x.ProductKey,
                    ProductName = x.ProductName,
                    Brand = x.Brand,
                    CategoryCode = x.CategoryCode,
                    Source = x.Source,
                    Currency = x.Currency,
                    Price = x.Price,
                    Median = x.Median,
                    LowerBound = x.LowerBound,
                    UpperBound = x.UpperBound,
                    DeviationPercentage = x.DeviationPercentage,
                    Severity = x.Severity,
                    Direction = x.Direction,
                    DetectedAt = x.DetectedAt
                })
                .ToList();

            return new PagedResult<PriceAnomalyDto>(
                items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}
