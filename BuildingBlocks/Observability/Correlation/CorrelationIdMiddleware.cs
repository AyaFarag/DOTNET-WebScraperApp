using Microsoft.AspNetCore.Http;
using Serilog.Context;
using BuildingBlocks.Observability.Abstractions.Correlation;

namespace BuildingBlocks.Observability.Correlation;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICorrelationContext correlation)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                            ?? Guid.NewGuid().ToString();

        correlation.Set(correlationId);

        context.Response.Headers["X-Correlation-ID"] = correlationId;

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
    }
}