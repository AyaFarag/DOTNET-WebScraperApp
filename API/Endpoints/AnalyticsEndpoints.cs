using Analytics.Application.CQRS.Dashboard.Queries;
using Analytics.Application.CQRS.Execution;
using Analytics.Application.CQRS.GetAnomalies;
using Analytics.Application.CQRS.MarketAnalysis.Queries;
using Analytics.Application.CQRS.PriceStatistics.Queries;
using Analytics.Application.CQRS.PriceStatistics.Queries.Product;
using Analytics.Application.CQRS.PriceTrend.Queries;
using Analytics.Application.Interfaces.AI.Repository;
using Analytics.Domain.Enums;
using MediatR;

namespace API.Endpoints
{
    public static class PriceStatisticsEndpoints
    {
        public static IEndpointRouteBuilder MapPriceStatisticsEndpoints( this IEndpointRouteBuilder endpoints)
        {
            // get single product price statistics
            endpoints.MapGet("/api/analytics/products/{productKey}/statistics", async ( 
                string productKey,  
                string? source, 
                string? currency, 
                ISender sender, 
                CancellationToken cancellationToken) =>
                {
                    var query =  new GetProductPriceStatisticsQuery(productKey, source, currency);

                    var result = await sender.Send(query, cancellationToken);

                    return result is null  ? Results.NotFound() : Results.Ok(result);
                });

            // get all products price statistics with optional filters
            endpoints.MapGet("/api/analytics/statistics", async (
                string? categoryCode,
                string? source,
                string? currency,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetPriceStatisticsQuery(categoryCode,source,currency);

                var result = await sender.Send(query,cancellationToken);

                return Results.Ok(result);
            });

            // GET /api/analytics/products/iphone-15/trend
            // ?from = 2026 - 09 - 01
            // & to = 2026 - 09 - 06
            // & source = Carrefour
            // & currency = AED
            // & granularity = Daily

            endpoints.MapGet("/api/analytics/products/{productKey}/trend",
                async (
                    string productKey,
                    DateTime from,
                    DateTime to,
                    string? source,
                    string? currency,
                    TrendGranularity granularity,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var query = new GetProductPriceTrendQuery(productKey,from,to, source,currency,granularity);

                    var result = await sender.Send(query,cancellationToken);

                    return Results.Ok(result);
                });

            // product's current price compare across suppliers
            endpoints.MapGet("/api/analytics/products/{productKey}/market", async (
                string productKey,
                string? currency,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetMarketAnalysisQuery(productKey,currency);

                var result = await sender.Send(query, cancellationToken);

                return result is null ? Results.NotFound() : Results.Ok(result);
            });


            endpoints.MapGet("/api/analytics/anomalies", async (
                string? productKey,
                string? categoryCode,
                string? source,
                string? severity,
                string? direction,
                DateTime? from,
                DateTime? to,
                int? page,
                int? pageSize,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAnomaliesQuery( productKey,categoryCode,source,severity, direction,from,to,page ?? 1,pageSize ?? 20);

                var result = await sender.Send(query,cancellationToken);

                return Results.Ok(result);
            });


            endpoints.MapGet("/api/analytics/anomalies/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAnomalyByIdQuery(id);

                var result = await sender.Send(query,cancellationToken);

                return result is null ? Results.NotFound() : Results.Ok(result);
            });


            endpoints.MapGet( "/api/analytics/dashboard", async (
                string? categoryCode,
                string? source,
                string? currency,
                DateTime? from,
                DateTime? to,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAnalyticsDashboardQuery(
                    categoryCode,
                    source,
                    currency,
                    from,
                    to);

                var result = await sender.Send(
                    query,
                    cancellationToken);

                return Results.Ok(result);
            });

            // GET /api/analytics/executions
            endpoints.MapGet("/api/analytics/executions", async ( 
                int? page, 
                int? pageSize, 
                string? status, 
                ISender sender, 
                CancellationToken cancellationToken) => 
            { 
                var query = new GetAnalyticsExecutionsQuery(Page: page ?? 1, PageSize: pageSize ?? 20, Status: status ); 
                var result = await sender.Send( query, cancellationToken); 
                
                return Results.Ok(result); 
            
            });

            // GET /api/analytics/executions/{id}
            endpoints.MapGet("/api/analytics/executions/{id:guid}", async (
                Guid id, 
                ISender sender, 
                CancellationToken cancellationToken) => { 
                    var query = new GetAnalyticsExecutionByIdQuery(id); 
                    var result = await sender.Send(query, cancellationToken); 
                
                    return result is null ? Results.NotFound() : Results.Ok(result); 
                });

            // ai agent get anomely explanation by anomaly id
            endpoints.MapGet("api/anomely/explaination/{id:guid}", async (Guid id, IAnomalyExplanationRepository repository,CancellationToken cancellationToken) =>
                {
                    var explanation = await repository.GetByAnomalyIdAsync(id, cancellationToken);

                    return explanation is null ? Results.NotFound() : Results.Ok(explanation);
                });

            return endpoints;
        }
    }
}
