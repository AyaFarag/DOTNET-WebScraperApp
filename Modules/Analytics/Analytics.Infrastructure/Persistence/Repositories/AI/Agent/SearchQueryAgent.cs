using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Agent;
using Analytics.Infrastructure.Persistence.Repositories.AI.Prompt;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Agent
{
    public sealed class SearchQueryAgent : ISearchQueryAgent
    {
        private readonly Kernel _kernel;

        public SearchQueryAgent(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task<SearchQueryPlan> CreatePlanAsync(string userQuery,CancellationToken cancellationToken)
        {
            ValidateUserQuery(userQuery);

            var chatService = GetChatCompletionService();

            var executionSettings = CreateExecutionSettings();

            var chatHistory = BuildChatHistory(userQuery);

            var response = await GetCompletionAsync(
                    chatService,
                    chatHistory,
                    executionSettings,
                    cancellationToken);

            var content = ExtractContent(response);

            var plan = DeserializeResult(content);

            ValidatePlan(plan);

            return plan;
        }

        // =========================================================
        // 1. Validate user query
        // =========================================================

        private static void ValidateUserQuery(
            string userQuery)
        {
            if (string.IsNullOrWhiteSpace(userQuery))
            {
                throw new ArgumentException(
                    "Search query cannot be empty.",
                    nameof(userQuery));
            }
        }

        // =========================================================
        // 2. Get Semantic Kernel chat service
        // =========================================================

        private IChatCompletionService
            GetChatCompletionService()
        {
            return _kernel
                .GetRequiredService<IChatCompletionService>();
        }

        // =========================================================
        // 3. Create execution settings
        // =========================================================

        private static OpenAIPromptExecutionSettings
            CreateExecutionSettings()
        {
            return new OpenAIPromptExecutionSettings
            {
                Temperature = 0,

                ResponseFormat =
                    typeof(SearchQueryPlan)
            };
        }

        // =========================================================
        // 4. Build chat history
        // =========================================================

        private static ChatHistory
            BuildChatHistory(
                string userQuery)
        {
            var chatHistory =
                new ChatHistory();

            AddSystemInstructions(
                chatHistory);

            AddUserPrompt(
                chatHistory,
                userQuery);

            return chatHistory;
        }

        // =========================================================
        // 5. Add system instructions
        // =========================================================

        private static void AddSystemInstructions(
            ChatHistory chatHistory)
        {
            chatHistory.AddSystemMessage(
                SearchQueryPrompt.SystemInstructions);
        }

        // =========================================================
        // 6. Add user prompt
        // =========================================================

        private static void AddUserPrompt(
            ChatHistory chatHistory,
            string userQuery)
        {
            var prompt =
                SearchQueryPrompt.Build(
                    userQuery);

            chatHistory.AddUserMessage(
                prompt);
        }

        // =========================================================
        // 7. Call LLM
        // =========================================================

        private static async Task<ChatMessageContent>
            GetCompletionAsync(
                IChatCompletionService chatService,
                ChatHistory chatHistory,
                OpenAIPromptExecutionSettings executionSettings,
                CancellationToken cancellationToken)
        {
            return await chatService
                .GetChatMessageContentAsync(
                    chatHistory,
                    executionSettings,
                    cancellationToken:
                        cancellationToken);
        }

        // =========================================================
        // 8. Extract response
        // =========================================================

        private static string ExtractContent(
            ChatMessageContent response)
        {
            if (string.IsNullOrWhiteSpace(
                    response.Content))
            {
                throw new InvalidOperationException(
                    "Search query agent returned an empty response.");
            }

            return response.Content.Trim();
        }

        // =========================================================
        // 9. Deserialize structured output
        // =========================================================

        private static SearchQueryPlan
            DeserializeResult(
                string content)
        {
            try
            {
                var result =
                    JsonSerializer.Deserialize<SearchQueryPlan>(
                        content,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (result is null)
                {
                    throw new InvalidOperationException(
                        "Search query plan could not be deserialized.");
                }

                return result;
            }
            catch (JsonException exception)
            {
                throw new InvalidOperationException(
                    "Search query agent returned invalid structured output.",
                    exception);
            }
        }

        // =========================================================
        // 10. Validate structured result
        // =========================================================

        private static void ValidatePlan(
            SearchQueryPlan plan)
        {
            if (plan.Page < 1)
            {
                throw new InvalidOperationException(
                    "Search query page must be greater than zero.");
            }

            if (plan.PageSize < 1)
            {
                throw new InvalidOperationException(
                    "Search query page size must be greater than zero.");
            }

            if (plan.PageSize > 100)
            {
                throw new InvalidOperationException(
                    "Search query page size cannot exceed 100.");
            }

            if (plan.MinimumPrice.HasValue &&
                plan.MaximumPrice.HasValue &&
                plan.MinimumPrice >
                plan.MaximumPrice)
            {
                throw new InvalidOperationException(
                    "Search query minimum price cannot exceed maximum price.");
            }
        }
    }
}
