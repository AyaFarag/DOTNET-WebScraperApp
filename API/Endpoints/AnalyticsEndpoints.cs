using Analytics.Application.CQRS.Dashboard.Queries;
using Analytics.Application.CQRS.Execution;
using Analytics.Application.CQRS.GetAnomalies;
using Analytics.Application.CQRS.MarketAnalysis.Queries;
using Analytics.Application.CQRS.PriceStatistics.Queries;
using Analytics.Application.CQRS.PriceStatistics.Queries.Product;
using Analytics.Application.CQRS.PriceTrend.Queries;
using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.AI.AnalyticsSearch;
using Analytics.Application.Interfaces.AI.AnomalyExplanation;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Weights.Service;
using Analytics.Domain.Enums;
using Azure.Search.Documents.Indexes.Models;
using MediatR;

namespace API.Endpoints
{
    public static class PriceStatisticsEndpoints
    {
        public static IEndpointRouteBuilder MapPriceStatisticsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            // get single product price statistics
            endpoints.MapGet("/api/analytics/products/{productKey}/statistics", async (
                string productKey,
                string? source,
                string? currency,
                ISender sender,
                CancellationToken cancellationToken) =>
                {
                    var query = new GetProductPriceStatisticsQuery(productKey, source, currency);

                    var result = await sender.Send(query, cancellationToken);

                    return result is null ? Results.NotFound() : Results.Ok(result);
                });

            // get all products price statistics with optional filters
            endpoints.MapGet("/api/analytics/statistics", async (
                string? categoryCode,
                string? source,
                string? currency,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetPriceStatisticsQuery(categoryCode, source, currency);

                var result = await sender.Send(query, cancellationToken);

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
                    var query = new GetProductPriceTrendQuery(productKey, from, to, source, currency, granularity);

                    var result = await sender.Send(query, cancellationToken);

                    return Results.Ok(result);
                });

            // product's current price compare across suppliers
            endpoints.MapGet("/api/analytics/products/{productKey}/market", async (
                string productKey,
                string? currency,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetMarketAnalysisQuery(productKey, currency);

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
                var query = new GetAnomaliesQuery(productKey, categoryCode, source, severity, direction, from, to, page ?? 1, pageSize ?? 20);

                var result = await sender.Send(query, cancellationToken);

                return Results.Ok(result);
            });


            endpoints.MapGet("/api/analytics/anomalies/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAnomalyByIdQuery(id);

                var result = await sender.Send(query, cancellationToken);

                return result is null ? Results.NotFound() : Results.Ok(result);
            });


            endpoints.MapGet("/api/analytics/dashboard", async (
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
                var query = new GetAnalyticsExecutionsQuery(Page: page ?? 1, PageSize: pageSize ?? 20, Status: status);
                var result = await sender.Send(query, cancellationToken);

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
            endpoints.MapGet("api/anomely/explaination/{id:guid}", async (Guid id, IAnomalyExplanationRepository repository, CancellationToken cancellationToken) =>
                {
                    var explanation = await repository.GetByAnomalyIdAsync(id, cancellationToken);

                    return explanation is null ? Results.NotFound() : Results.Ok(explanation);
                });

            // price relative endpoints
            endpoints.MapGet("/api/analytics/price-relative/products/{productKey}", GetProductPriceRelative);

            endpoints.MapGet("/api/analytics/price-relative/category", GetCategoryPriceRelative);

            // price weight endpoints
            endpoints.MapGet(
               "/api/analytics/weights",
               GetWeights);

            endpoints.MapGet(
                "/api/analytics/weights/{id:guid}",
                GetById);

            endpoints.MapGet(
                "/api/analytics/weights/products/{productKey}",
                GetEffectiveProductWeight);

            endpoints.MapGet(
                "/api/analytics/weights/effective",
                GetEffectiveWeights);

            endpoints.MapPost(
                "/api/analytics/weights",
                UpsertWeight);

            endpoints.MapGet(
                "/validate",
                ValidateWeightSet);

            // weighted price endpoints
            endpoints.MapGet(
                "/products/{productKey}",
                GetProductWeightedPrice);

            endpoints.MapGet(
                "/category",
                GetCategoryWeightedPrices);

            endpoints.MapGet(
                 "/api/analytics/category-indexes",
                 GetIndexes);

            endpoints.MapGet(
                "/api/analytics/category-indexes/calculate",
                CalculateIndex);

            endpoints.MapGet(
            "/api/analytics/laspeyres",
            GetLaspeyresIndexes);

            endpoints.MapGet(
                "/api/analytics/laspeyres/calculate",
                CalculateLaspeyresIndex);

            endpoints.MapGet(
                "/api/analytics/paasch",
                GetPaaschIndexes);

            endpoints.MapGet(
                "/api/analytics/paasch/calculate",
                CalculatePaaschIndex);

            endpoints.MapGet(
                "/api/analytics/fisher",
                GetfisherIndexes);

            endpoints.MapGet(
                "/api/analytics/fisher/calculate",
                fisherCalculate);

            endpoints.MapGet(
                "/api/analytics/cpi",
                GetCPIIndexes);

            endpoints.MapGet(
                "/api/analytics/cpi/calculate",
                CPICalculate);

            endpoints.MapGet(
                "/api/analytics/ppi",
                GetPPIIndexes);

            endpoints.MapGet(
                "/api/analytics/ppi/calculate",
                PPICalculate);

            endpoints.MapGet(
                "/api/analytics/search",
                SearchAsync);

            return endpoints;
        }

        private static async Task<IResult> GetProductPriceRelative(
        string productKey,
        string? source,
        string currency,
        DateTime baseDate,
        DateTime currentDate,
        IPriceRelativeService service,
        CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    productKey,
                    source,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        }


        private static async Task<IResult> GetCategoryPriceRelative(
        string? categoryCode,
        string? source,
        string currency,
        DateTime baseDate,
        DateTime currentDate,
        IPriceRelativeService service,
        CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    categoryCode,
                    source,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> GetWeights(
        string? categoryCode,
        WeightType? weightType,
        DateTime? effectiveFrom,
        DateTime? effectiveTo,
        IPriceWeightService service,
        CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    categoryCode,
                    weightType,
                    effectiveFrom,
                    effectiveTo,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> GetById(
            Guid id,
            IPriceWeightService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.GetByIdAsync(
                    id,
                    cancellationToken);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        }

        private static async Task<IResult>
            GetEffectiveProductWeight(
                string productKey,
                DateTime date,
                IPriceWeightService service,
                CancellationToken cancellationToken)
        {
            var result =
                await service.GetEffectiveWeightAsync(
                    productKey,
                    date,
                    cancellationToken);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        }

        private static async Task<IResult>
            GetEffectiveWeights(
                string? categoryCode,
                DateTime date,
                IPriceWeightService service,
                CancellationToken cancellationToken)
        {
            var result =
                await service.GetEffectiveWeightsAsync(
                    categoryCode,
                    date,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> UpsertWeight(
            UpsertPriceWeightRequest request,
            IPriceWeightService service,
            CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await service.UpsertAsync(
                        request,
                        cancellationToken);

                return Results.Created(
                    $"/api/analytics/weights/{result.Id}",
                    result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }

        private static async Task<IResult> ValidateWeightSet(
            string categoryCode,
            DateTime date,
            IPriceWeightService service,
            CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await service.ValidateWeightSetAsync(
                        categoryCode,
                        date,
                        cancellationToken);

                return Results.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }


        private static async Task<IResult> GetProductWeightedPrice(
            string productKey,
            string? source,
            string currency,
            DateTime date,
            IWeightedPriceService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    productKey,
                    source,
                    currency,
                    date,
                    cancellationToken);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        }

        private static async Task<IResult> GetCategoryWeightedPrices(
                string categoryCode,
                string? source,
                string currency,
                DateTime date,
                IWeightedPriceService service,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await service.CalculateAsync(
                        categoryCode,
                        source,
                        currency,
                        date,
                        cancellationToken);

                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }

        private static async Task<IResult> GetIndexes(
           string currency,
           string? categoryCode,
           DateTime? from,
           DateTime? to,
           ICategoryIndexService service,
           CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> CalculateIndex(
         string categoryCode,
         string currency,
         DateTime baseDate,
         DateTime currentDate,
         ICategoryIndexService service,
         CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await service.CalculateAsync(
                        categoryCode,
                        currency,
                        baseDate,
                        currentDate,
                        cancellationToken);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }

        private static async Task<IResult> GetLaspeyresIndexes(
          string currency,
          string? categoryCode,
          DateTime? from,
          DateTime? to,
          ILaspeyresIndexService service,
          CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return Results.Ok(result);
        }


        private static async Task<IResult> CalculateLaspeyresIndex(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            ILaspeyresIndexService service,
            CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await service.CalculateAsync(
                        categoryCode,
                        currency,
                        baseDate,
                        currentDate,
                        cancellationToken);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }


        private static async Task<IResult> CalculatePaaschIndex(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            IPaascheIndexService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        }

        private static async Task<IResult> GetPaaschIndexes(
            string currency,
            string? categoryCode,
            DateTime? from,
            DateTime? to,
            IPaascheIndexService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> fisherCalculate(
           string categoryCode,
           string currency,
           DateTime baseDate,
           DateTime currentDate,
           IFisherIndexService service,
           CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        }


        private static async Task<IResult> GetfisherIndexes(
            string currency,
            string? categoryCode,
            DateTime? from,
            DateTime? to,
            IFisherIndexService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> CPICalculate(
           string currency,
           DateTime baseDate,
           DateTime currentDate,
           IConsumerPriceIndexService service,
           CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        }

        private static async Task<IResult> GetCPIIndexes(
            string currency,
            DateTime? from,
            DateTime? to,
            IConsumerPriceIndexService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    currency,
                    from,
                    to,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> PPICalculate(
          string currency,
          DateTime baseDate,
          DateTime currentDate,
          IProducerPriceIndexService service,
          CancellationToken cancellationToken)
        {
            var result =
                await service.CalculateAsync(
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        }

        private static async Task<IResult> GetPPIIndexes(
            string currency,
            DateTime? from,
            DateTime? to,
            IProducerPriceIndexService service,
            CancellationToken cancellationToken)
        {
            var result =
                await service.GetAsync(
                    currency,
                    from,
                    to,
                    cancellationToken);

            return Results.Ok(result);
        }

        private static async Task<IResult> SearchAsync(string q,int page,int pageSize,IAnalyticsSearchService searchService,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return Results.BadRequest(
                    new
                    {
                        message =
                            "Search query is required."
                    });
            }

            if (page < 1)
            {
                return Results.BadRequest(
                    new
                    {
                        message =
                            "Page must be greater than zero."
                    });
            }

            if (pageSize < 1 || pageSize > 100)
            {
                return Results.BadRequest(
                    new
                    {
                        message =
                            "Page size must be between 1 and 100."
                    });
            }

            try
            {
                var result = await searchService.SearchAsync(q,page,pageSize,cancellationToken);

                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }
    }
}
