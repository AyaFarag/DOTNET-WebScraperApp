using Analytics.Application.Interfaces;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services
{
    public sealed class IqrAnomalyDetector : IAnomalyDetector
    {
        private const decimal IqrMultiplier = 1.5m;
        


        public AnomalyDetectionResult Detect(decimal currentPrice, IReadOnlyCollection<decimal> historicalPrices)
        {
            if (historicalPrices is null || historicalPrices.Count < 4)
            {
                throw new ArgumentException(
                    "At least 4 historical prices are required.",
                    nameof(historicalPrices));
            }

            var prices = historicalPrices.OrderBy(x => x).ToArray();

            var q1 = CalculatePercentile(prices, 0.25m);
            var median = CalculatePercentile(prices, 0.50m);
            var q3 = CalculatePercentile(prices, 0.75m);

            var iqr = q3 - q1;

            var lowerBound = q1 - (IqrMultiplier * iqr);
            var upperBound = q3 + (IqrMultiplier * iqr);

            var direction = DetermineDirection(currentPrice,lowerBound,upperBound);

            var isAnomaly = direction != "Normal";

            var severity = DetermineSeverity(currentPrice,lowerBound,upperBound);

            decimal? deviationPercentage = null;

            if (median != 0)
            {
                deviationPercentage = ((currentPrice - median) / median) * 100;
            }

            return new AnomalyDetectionResult
            {
                IsAnomaly = isAnomaly,

                Severity = severity,

                Direction = direction,

                Price = currentPrice,

                Q1 = q1,

                Median = median,

                Q3 = q3,

                Iqr = iqr,

                LowerBound = lowerBound,

                UpperBound = upperBound,

                DeviationPercentage = deviationPercentage
            };
        }

        private static decimal CalculatePercentile(decimal[] sortedValues,decimal percentile)
        {
            var position = (sortedValues.Length - 1) * percentile;
            var lowerIndex = (int)Math.Floor(position);
            var upperIndex = (int)Math.Ceiling(position);

            if (lowerIndex == upperIndex)
            {
                return sortedValues[lowerIndex];
            }

            var weight = position - lowerIndex;

            return sortedValues[lowerIndex]
                   + ((sortedValues[upperIndex]
                   - sortedValues[lowerIndex]) * weight);
        }

        private static string DetermineDirection(decimal currentPrice,decimal lowerBound,decimal upperBound)
        {
            if (currentPrice > upperBound)
            {
                return "HighPrice";
            }

            if (currentPrice < lowerBound)
            {
                return "LowPrice";
            }

            return "Normal";
        }

        private static string DetermineSeverity(decimal currentPrice, decimal lowerBound, decimal upperBound)
        {
            if (currentPrice >= lowerBound &&
                currentPrice <= upperBound)
            {
                return "Normal";
            }

            var distance = currentPrice < lowerBound ? lowerBound - currentPrice : currentPrice - upperBound;

            var range = upperBound - lowerBound;

            if (range <= 0)
            {
                return "High";
            }

            var ratio = distance / range;

            return ratio switch
            {
                >= 1.0m => "Critical",
                >= 0.5m => "High",
                _ => "Medium"
            };
        }
    }
}
