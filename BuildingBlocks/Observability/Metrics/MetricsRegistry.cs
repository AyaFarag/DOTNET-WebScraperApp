using Prometheus;

public static class MetricsRegistry
{
    public static readonly Counter HttpRequests =
        Metrics.CreateCounter(
            "http_requests_total",
            "Total number of HTTP requests");

    public static readonly Histogram RequestDuration =
        Metrics.CreateHistogram(
            "http_request_duration_seconds",
            "HTTP request duration in seconds");
}
