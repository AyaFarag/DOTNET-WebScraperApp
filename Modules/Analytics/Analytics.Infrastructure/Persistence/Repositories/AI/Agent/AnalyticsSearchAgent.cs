using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.AnalyticsSearch;
using Analytics.Domain.Enums;
using Analytics.Infrastructure.Persistence.Repositories.AI.Prompt;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Agent
{
    public sealed class AnalyticsSearchAgent : IAnalyticsSearchQueryAgent
    {
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatCompletionService;

        public AnalyticsSearchAgent(
            Kernel kernel,
            IChatCompletionService chatCompletionService)
        {
            _kernel = kernel;
            _chatCompletionService = chatCompletionService;
        }

        public async Task<AnalyticsSearchQueryPlan> CreatePlanAsync(string query, CancellationToken cancellationToken)
        {
            var settings = CreateExecutionSettings();

            var history = BuildChatHistory(query);

            var response = await InvokeAsync(
                history,
                settings,
                cancellationToken);

            var content = ExtractContent(response);

            var plan = Deserialize(content);

            Validate(plan);

            return plan;
        }

        private static PromptExecutionSettings CreateExecutionSettings()
        {
            return new OpenAIPromptExecutionSettings
            {
                Temperature = 0
            };
        }

        private static ChatHistory BuildChatHistory(string query)
        {
            var history = new ChatHistory();

            history.AddSystemMessage(
                AnalyticsSearchPrompt.System);

            history.AddUserMessage(query);

            return history;
        }

        private async Task<ChatMessageContent> InvokeAsync(ChatHistory history, PromptExecutionSettings settings,
            CancellationToken cancellationToken)
        {
            return await _chatCompletionService.GetChatMessageContentAsync(
                history,
                settings,
                _kernel,
                cancellationToken);
        }

        private static string ExtractContent(ChatMessageContent response)
        {
            return response.Content
                ?? throw new InvalidOperationException(
                    "Analytics Search Agent returned empty content.");
        }

        private static AnalyticsSearchQueryPlan Deserialize(string content)
        {
            var plan =
                JsonSerializer.Deserialize<AnalyticsSearchQueryPlan>(
                    content,
                    JsonSerializerOptions.Default);

            return plan
                ?? throw new InvalidOperationException(
                    "Failed to deserialize analytics search plan.");
        }

        private static void Validate(AnalyticsSearchQueryPlan plan)
        {
            if (plan.Intent == AnalyticsSearchIntent.PriceStatistics &&
                string.IsNullOrWhiteSpace(plan.ProductKey))
            {
                throw new InvalidOperationException(
                    "ProductKey is required for price statistics.");
            }
        }
    }
}
