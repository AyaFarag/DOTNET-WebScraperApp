using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using Analytics.Infrastructure.Persistence.Repositories.AI.Prompt;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Agent
{
    public sealed class PriceSnapshotSearchAgent : IPriceSnapshotSearchAgent
    {
        private readonly Kernel _kernel;

        private readonly IChatCompletionService _chatCompletionService;

        public PriceSnapshotSearchAgent(Kernel kernel, IChatCompletionService chatCompletionService)
        {
            _kernel = kernel;
            _chatCompletionService = chatCompletionService;
        }

        public async Task<PriceSnapshotSearchRequest> CreateRequestAsync(AnalyticsSearchQueryPlan plan, int page, int pageSize, CancellationToken cancellationToken)
        {
            var settings = CreateExecutionSettings();

            var history = BuildChatHistory(
                plan,
                page,
                pageSize);

            var response = await InvokeAsync(
                history,
                settings,
                cancellationToken);

            var content = ExtractContent(response);

            var request = Deserialize(content);

            Validate(request);

            return request;
        }

        private static OpenAIPromptExecutionSettings CreateExecutionSettings()
        {
            return new OpenAIPromptExecutionSettings
            {
                Temperature = 0
            };
        }

        private static ChatHistory BuildChatHistory(AnalyticsSearchQueryPlan plan, int page, int pageSize)
        {
            var history = new ChatHistory();

            history.AddSystemMessage(
                PriceSnapshotSearchPrompt.System);

            var input = new
            {
                Plan = plan,
                Page = page,
                PageSize = pageSize
            };

            history.AddUserMessage(JsonSerializer.Serialize(input, JsonSerializerOptions.Default));

            return history;
        }

        private async Task<ChatMessageContent> InvokeAsync(ChatHistory history, OpenAIPromptExecutionSettings settings, CancellationToken cancellationToken)
        {
            return await _chatCompletionService.GetChatMessageContentAsync(history,settings, _kernel,cancellationToken);
        }

        private static string ExtractContent(ChatMessageContent response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new InvalidOperationException(
                    "Price Snapshot Search Agent returned empty content.");
            }

            return response.Content;
        }

        private static PriceSnapshotSearchRequest Deserialize(
            string content)
        {
            var request =
                JsonSerializer.Deserialize<
                    PriceSnapshotSearchRequest>(
                    content,
                    JsonSerializerOptions.Default);

            return request
                ?? throw new InvalidOperationException(
                    "Failed to deserialize Price Snapshot Search Agent response.");
        }

        private static void Validate(
            PriceSnapshotSearchRequest request)
        {
            if (request.Take < 1)
            {
                throw new InvalidOperationException(
                    "Search result limit must be greater than zero.");
            }

            if (request.Take > 100)
            {
                throw new InvalidOperationException(
                    "Search result limit cannot exceed 100.");
            }

            if (request.Skip < 0)
            {
                throw new InvalidOperationException(
                    "Search skip cannot be negative.");
            }

            if (request.From.HasValue &&
                request.To.HasValue &&
                request.From.Value > request.To.Value)
            {
                throw new InvalidOperationException(
                    "Search start date cannot be after search end date.");
            }
        }
    }
}
