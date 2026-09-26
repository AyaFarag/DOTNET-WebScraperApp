using Analytics.Application.DTOs;
using Analytics.Application.Interfaces;
using Analytics.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Services
{
    public sealed class MarketAnalysisService : IMarketAnalysisService
    {
        private readonly IPriceSnapshotRepository _snapshotRepository;

        public MarketAnalysisService(IPriceSnapshotRepository snapshotRepository)
        {
            _snapshotRepository = snapshotRepository;
        }

        public async Task<MarketAnalysisDto?> AnalyzeProductAsync(string productKey,string? currency,CancellationToken cancellationToken)
        {
            var snapshots = await _snapshotRepository.GetLatestPricesByProductAsync(productKey,currency,cancellationToken);

            if (snapshots.Count == 0)
            {
                return null;
            }

            var marketAverage = snapshots.Average(x => x.Price);

            var marketMinimum = snapshots.Min(x => x.Price);

            var marketMaximum = snapshots.Max(x => x.Price);

            var priceSpread = marketMaximum - marketMinimum;

            decimal? spreadPercentage = null;

            if (marketMinimum != 0)
            {
                spreadPercentage = (priceSpread / marketMinimum) * 100;
            }

            var suppliers = snapshots.OrderBy(x => x.Price)
                .Select((snapshot, index) =>
                {
                    var difference = snapshot.Price - marketAverage;

                    decimal? differencePercentage = null;

                    if (marketAverage != 0)
                    {
                        differencePercentage = (difference / marketAverage) * 100;
                    }

                    return new SupplierPriceDto
                    {
                        Source = snapshot.Source ?? "Unknown",

                        Price = snapshot.Price,

                        DifferenceFromMarketAverage = difference,

                        DifferencePercentage = differencePercentage,

                        Rank = index + 1
                    };
                })
                .ToList();

            var cheapestSupplier = suppliers.FirstOrDefault();

            var mostExpensiveSupplier = suppliers.LastOrDefault();

            var firstSnapshot = snapshots.First();

            return new MarketAnalysisDto
            {
                ProductKey = productKey,

                ProductName = firstSnapshot.ProductName,

                Brand = firstSnapshot.Brand,

                Currency = currency,

                MarketAveragePrice = marketAverage,

                MarketMinimumPrice = marketMinimum,

                MarketMaximumPrice = marketMaximum,

                PriceSpread = priceSpread,

                PriceSpreadPercentage = spreadPercentage,

                CheapestSupplier = cheapestSupplier,

                MostExpensiveSupplier = mostExpensiveSupplier,

                Suppliers = suppliers
            };
        }
    }
}
