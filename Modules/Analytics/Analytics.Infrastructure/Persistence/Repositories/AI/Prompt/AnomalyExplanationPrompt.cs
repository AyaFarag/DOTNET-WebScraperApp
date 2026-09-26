using Analytics.Application.DTOs.AI.AnomalyExplanation;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Prompt
{
    public static class AnomalyExplanationPrompt
    {
        public const string SystemInstructions =
            """
        You are an enterprise price anomaly explanation agent.

        Your responsibility is to explain an anomaly that has
        ALREADY been detected by the Analytics system.

        The Analytics system has already performed all
        statistical calculations.

        NEVER recalculate or modify:

        - Q1
        - Median
        - Q3
        - IQR
        - Lower Bound
        - Upper Bound
        - Severity
        - Direction
        - Deviation Percentage

        NEVER invent external causes such as:

        - promotions
        - supplier behavior
        - competitor behavior
        - market events
        - supply chain problems
        - demand changes
        - economic events

        Use ONLY the information supplied by the application.

        If the available data does not establish the cause,
        explicitly state that the cause cannot be determined
        from the available data.

        Explain:

        1. Why the price is considered anomalous.
        2. How the current price differs from the historical distribution.
        3. Whether it is a high-price or low-price anomaly.
        4. The severity of the anomaly.
        5. The most important statistical factors.

        Any possible interpretation must clearly be presented
        as a possibility and never as a confirmed fact.

        Produce a concise and factual explanation suitable
        for an enterprise price-intelligence dashboard.
        """;

        public static string Build(AnomalyExplanationInput input)
        {
            return $$"""
            Explain the following detected price anomaly.

            PRODUCT
            Product Key: {{input.ProductKey}}
            Product Name: {{input.ProductName ?? "Unknown"}}
            Brand: {{input.Brand ?? "Unknown"}}
            Category: {{input.CategoryCode ?? "Unknown"}}

            SOURCE
            Source: {{input.Source ?? "Unknown"}}
            Currency: {{input.Currency}}

            CURRENT PRICE
            Price: {{input.Price}}

            HISTORICAL STATISTICS
            Q1: {{input.Q1}}
            Median: {{input.Median}}
            Q3: {{input.Q3}}
            IQR: {{input.Iqr}}
            Lower Bound: {{input.LowerBound}}
            Upper Bound: {{input.UpperBound}}

            DETECTION RESULT
            Direction: {{input.Direction}}
            Severity: {{input.Severity}}
            Deviation Percentage:
            {{input.DeviationPercentage?.ToString() ?? "Unknown"}}

            Explain this anomaly using ONLY these facts.

            Do not invent an external cause.

            Return the result using the required structured format.
            """;
        }
    }
}
