using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Enums;

namespace Analytics.Application.Services.PriceAnalytics
{
    public sealed class PriceTrendService : IPriceTrendService
    {
        private readonly IPriceSnapshotRepository _snapshotRepository;

        public PriceTrendService(IPriceSnapshotRepository snapshotRepository)
        {
            _snapshotRepository = snapshotRepository;
        }

        public async Task<IReadOnlyCollection<PriceTrendDto>> GetProductTrendAsync(
            string productKey, 
            DateTime from,
            DateTime to,
            string? source,
            string? currency,
            TrendGranularity granularity,
            CancellationToken cancellationToken)
        {
            var snapshots = await _snapshotRepository.GetProductHistoryAsync(productKey,from,to,source,currency,cancellationToken);

            if (snapshots.Count == 0)
            {
                return [];
            }

            var groups = snapshots.GroupBy(x => GetPeriodStart(x.CollectedAt, granularity)).OrderBy(x => x.Key);

            var result = new List<PriceTrendDto>();

            foreach (var group in groups)
            {
                var orderedSnapshots = group.OrderBy(x => x.CollectedAt).ToList();

                var openingPrice = orderedSnapshots.First().Price;

                var closingPrice = orderedSnapshots.Last().Price;

                var priceChange = closingPrice - openingPrice;

                decimal? percentage = null;

                if (openingPrice != 0)
                {
                    percentage = (priceChange / openingPrice) * 100;
                }

                result.Add(new PriceTrendDto
                {
                    ProductKey = productKey,

                    ProductName = orderedSnapshots.First().ProductName,

                    Brand = orderedSnapshots.First().Brand,

                    Source = orderedSnapshots.First().Source,

                    Currency = currency,

                    PeriodStart = group.Key,

                    PeriodEnd =  GetPeriodEnd(group.Key,granularity),

                    OpeningPrice = openingPrice,

                    ClosingPrice = closingPrice,

                    MinimumPrice = orderedSnapshots.Min(x => x.Price),

                    MaximumPrice = orderedSnapshots.Max(x => x.Price),

                    AveragePrice = orderedSnapshots.Average(x => x.Price),

                    PriceChange = priceChange,

                    PriceChangePercentage = percentage
                });
            }

            return result;
        }

        private static DateTime GetPeriodStart( DateTime date, TrendGranularity granularity)
        {
            return granularity switch
            {
                TrendGranularity.Daily =>
                    date.Date,

                TrendGranularity.Weekly => StartOfWeek(date),

                TrendGranularity.Monthly =>
                    new DateTime(
                        date.Year,
                        date.Month,
                        1),

                _ => date.Date
            };
        }

        private static DateTime GetPeriodEnd(DateTime periodStart,TrendGranularity granularity)
        {
            return granularity switch
            {
                TrendGranularity.Daily =>
                    periodStart.AddDays(1).AddTicks(-1),

                TrendGranularity.Weekly =>
                    periodStart.AddDays(7).AddTicks(-1),

                TrendGranularity.Monthly =>
                    periodStart.AddMonths(1).AddTicks(-1),

                _ => periodStart
            };
        }

        private static DateTime StartOfWeek(DateTime date)
        {
            var difference =
                (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;

            return date.Date.AddDays(-difference);
        }
    }
}
