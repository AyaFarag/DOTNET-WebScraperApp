using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Domain.Entities;
using Analytics.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Repositories;

public sealed class AnalyticsDashboardRepository : IAnalyticsDashboardRepository
{
    private readonly AnalyticsDbContext _context;

    public AnalyticsDashboardRepository(
        AnalyticsDbContext context)
    {
        _context = context;
    }

    public async Task<AnalyticsDashboardData> GetDashboardDataAsync(
        string? categoryCode,
        string? source,
        string? currency,
        DateTime? from,
        DateTime? to,
        int topCount,
        CancellationToken cancellationToken)
    {
        // --------------------------------------------------
        // Price Snapshots
        // --------------------------------------------------

        var snapshotsQuery = _context.PriceSnapshots.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoryCode))
        {
            snapshotsQuery = snapshotsQuery.Where(x => x.CategoryCode == categoryCode);
        }

        if (!string.IsNullOrWhiteSpace(source))
        {
            snapshotsQuery = snapshotsQuery.Where(x => x.Source == source);
        }

        if (!string.IsNullOrWhiteSpace(currency))
        {
            snapshotsQuery = snapshotsQuery.Where(x => x.Currency == currency);
        }

        if (from.HasValue)
        {
            snapshotsQuery = snapshotsQuery.Where(x => x.CollectedAt >= from.Value);
        }

        if (to.HasValue)
        {
            snapshotsQuery = snapshotsQuery.Where(x => x.CollectedAt <= to.Value);
        }


        // --------------------------------------------------
        // Basic Statistics
        // --------------------------------------------------

        var totalProducts = await snapshotsQuery
            .Select(x => x.ProductKey)
            .Distinct()
            .CountAsync(cancellationToken);

        var totalSources = await snapshotsQuery
            .Where(x => x.Source != null)
            .Select(x => x.Source!)
            .Distinct()
            .CountAsync(cancellationToken);

        var averageMarketPrice = await snapshotsQuery
            .Select(x => (decimal?)x.Price)
            .AverageAsync(cancellationToken)
            ?? 0m;


        // --------------------------------------------------
        // Anomalies
        // --------------------------------------------------

        var anomaliesQuery = _context.PriceAnomalies
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoryCode))
        {
            anomaliesQuery = anomaliesQuery.Where(x => x.CategoryCode == categoryCode);
        }

        if (!string.IsNullOrWhiteSpace(source))
        {
            anomaliesQuery = anomaliesQuery.Where(x => x.Source == source);
        }

        if (!string.IsNullOrWhiteSpace(currency))
        {
            anomaliesQuery = anomaliesQuery.Where(x => x.Currency == currency);
        }

        if (from.HasValue)
        {
            anomaliesQuery = anomaliesQuery.Where(x => x.DetectedAt >= from.Value);
        }

        if (to.HasValue)
        {
            anomaliesQuery = anomaliesQuery.Where(x => x.DetectedAt <= to.Value);
        }

        var totalAnomalies = await anomaliesQuery.CountAsync(cancellationToken);

        var criticalAnomalies = await anomaliesQuery.CountAsync( x => x.Severity == "Critical",cancellationToken);

        var highPriceAnomalies = await anomaliesQuery.CountAsync( x => x.Direction == "HighPrice", cancellationToken);

        var lowPriceAnomalies = await anomaliesQuery.CountAsync( x => x.Direction == "LowPrice", cancellationToken);


        // --------------------------------------------------
        // Price Changes
        // --------------------------------------------------

        var priceChanges = await GetPriceChangesAsync(snapshotsQuery, cancellationToken);


        var topPriceIncreases = priceChanges
            .Where(x => x.ChangePercentage > 0)
            .OrderByDescending(x => x.ChangePercentage)
            .Take(topCount)
            .ToList();

        var topPriceDecreases = priceChanges
            .Where(x => x.ChangePercentage < 0)
            .OrderBy(x => x.ChangePercentage)
            .Take(topCount)
            .ToList();


        // --------------------------------------------------
        // Result
        // --------------------------------------------------

        return new AnalyticsDashboardData
        {
            TotalProducts = totalProducts,
            TotalSources = totalSources,
            AverageMarketPrice = averageMarketPrice,

            TotalAnomalies = totalAnomalies,
            CriticalAnomalies = criticalAnomalies,
            HighPriceAnomalies = highPriceAnomalies,
            LowPriceAnomalies = lowPriceAnomalies,

            TopPriceIncreases = topPriceIncreases,
            TopPriceDecreases = topPriceDecreases
        };
    }


    private static async Task<List<PriceChangeSummaryDto>> GetPriceChangesAsync( IQueryable<PriceSnapshot> query,CancellationToken cancellationToken)
    {
        var snapshots = await query
            .OrderByDescending(x => x.CollectedAt)
            .ToListAsync(cancellationToken);

        var changes = new List<PriceChangeSummaryDto>();

        var groups = snapshots
            .Where(x => x.Source != null)
            .GroupBy(x => new
            {
                x.ProductKey,
                x.Source,
                x.Currency
            });

        foreach (var group in groups)
        {
            var ordered = group
                .OrderByDescending(x => x.CollectedAt)
                .Take(2)
                .ToList();

            if (ordered.Count < 2)
                continue;

            var current = ordered[0];
            var previous = ordered[1];

            if (previous.Price == 0)
                continue;

            var changePercentage = ((current.Price - previous.Price)  / previous.Price) * 100m;

            changes.Add(new PriceChangeSummaryDto
            {
                ProductKey = current.ProductKey,
                ProductName = current.ProductName,
                CurrentPrice = current.Price,
                PreviousPrice = previous.Price,
                ChangePercentage = changePercentage,
                Source = current.Source,
                Currency = current.Currency
            });
        }

        return changes;
    }
}
