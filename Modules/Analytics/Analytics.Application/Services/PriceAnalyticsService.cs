using Analytics.Application.DTOs;
using Analytics.Application.Interfaces;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services
{
    public sealed class PriceAnalyticsService : IPriceAnalyticsService
    {
        private readonly IPriceSnapshotRepository _snapshotRepository;

        public PriceAnalyticsService(IPriceSnapshotRepository snapshotRepository)
        {
            _snapshotRepository = snapshotRepository;
        }

        public async Task<PriceStatisticsDto?> GetProductStatisticsAsync(string productKey, string? source, string? currency ,CancellationToken cancellationToken)
        {
            var snapshots = await _snapshotRepository.GetByProductKeyAsync(productKey, source, currency, cancellationToken);

            if (snapshots.Count == 0)
                return null;

            return CalculateStatistics(snapshots);
        }

        public async Task<IReadOnlyCollection<PriceStatisticsDto>> GetStatisticsAsync(string? categoryCode, string? source, string? currency, CancellationToken cancellationToken)
        {
            var snapshots = await _snapshotRepository.GetForAnalyticsAsync(categoryCode,source,currency, cancellationToken);

            return snapshots.GroupBy(x => x.ProductKey).Select(group => CalculateStatistics(group)).ToList();
        }

        private static PriceStatisticsDto CalculateStatistics(IEnumerable<PriceSnapshot> snapshots)
        {
            var ordered = snapshots.OrderBy(x => x.CollectedAt).ToList();

            var prices = ordered.Select(x => x.Price).OrderBy(x => x).ToList();

            var current = ordered[^1];

            var previous = ordered.Count > 1 ? ordered[^2] : null;

            var average = prices.Average();

            var median = CalculateMedian(prices);

            decimal? priceChange = null;
            decimal? priceChangePercentage = null;

            if (previous is not null)
            {
                priceChange = current.Price - previous.Price;

                if (previous.Price != 0)
                {
                    priceChangePercentage = priceChange / previous.Price * 100;
                }
            }

            return new PriceStatisticsDto
            {
                ProductKey = current.ProductKey,
                ProductName = current.ProductName,
                Brand = current.Brand,
                CategoryCode = current.CategoryCode,
                Source = current.Source,
                Currency = current.Currency,

                CurrentPrice = current.Price,

                PreviousPrice = previous?.Price,

                MinimumPrice = prices.Min(),

                MaximumPrice = prices.Max(),

                AveragePrice = Math.Round(average, 4),

                MedianPrice = Math.Round(median, 4),

                PriceChange = priceChange,

                PriceChangePercentage = priceChangePercentage,

                ObservationCount = ordered.Count,

                LastCollectedAt = current.CollectedAt
            };
        }

        private static decimal CalculateMedian(IReadOnlyList<decimal> values)
        {
            if (values.Count == 0)
                return 0;

            var middle = values.Count / 2;

            if (values.Count % 2 == 0)
            {
                return (values[middle - 1] + values[middle]) / 2;
            }

            return values[middle];
        }
    }
}
