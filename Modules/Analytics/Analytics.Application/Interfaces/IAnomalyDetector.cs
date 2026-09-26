using Analytics.Application.DTOs;
using Analytics.Domain.Entities;

namespace Analytics.Application.Interfaces
{
    public interface IAnomalyDetector
    {
        AnomalyDetectionResult Detect(decimal currentPrice, IReadOnlyCollection<decimal> historicalPrices);
    }
}
