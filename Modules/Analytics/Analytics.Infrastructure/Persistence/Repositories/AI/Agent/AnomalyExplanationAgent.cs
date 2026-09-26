using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text.Json;
using Analytics.Infrastructure.Persistence.Repositories.AI.Prompt;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Analytics.Application.DTOs.AI.AnomalyExplanation;
using Analytics.Application.Interfaces.AI.Agent;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Agent
{

    public sealed class AnomalyExplanationAgent : IAnomalyExplanationAgent
    {
        private readonly Kernel _kernel;

        public AnomalyExplanationAgent(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task<AnomalyExplanationResult> ExplainAsync(AnomalyExplanationInput input,
            CancellationToken cancellationToken)
        {
            // 1. Get Chat Completion Service
            var chatService = GetChatCompletionService();
            // 2. Create OpenAI execution settings
            var executionSettings = CreateExecutionSettings();
            // 3. Build Chat History
            // 4. Add system instructions
            // 5. Add anomaly prompt
            var chatHistory = BuildChatHistory(input);
            // 6. Call the LLM
            var response = await GetCompletionAsync(chatService, chatHistory, executionSettings, cancellationToken);
            // 7. Extract response content
            var content = ExtractContent(response);
            // 8. Deserialize structured response
            var result = DeserializeResult(content);
            // 9. Validate result
            ValidateResult(result);

            return result;
        }
        // =========================================================
        // 1. Get Chat Completion Service
        // =========================================================

        private IChatCompletionService GetChatCompletionService()
        {
            var service = _kernel.GetRequiredService<IChatCompletionService>();

            return service;
        }

        // =========================================================
        // 2. Create OpenAI execution settings
        // =========================================================

        private static OpenAIPromptExecutionSettings CreateExecutionSettings()
        {
            return new OpenAIPromptExecutionSettings
            {
                Temperature = 0,

                ResponseFormat =
                    typeof(AnomalyExplanationResult)
            };
        }

        // =========================================================
        // 3. Build Chat History
        // =========================================================

        private static ChatHistory BuildChatHistory(AnomalyExplanationInput input)
        {
            var chatHistory = new ChatHistory();

            AddSystemMessage(chatHistory);

            AddUserMessage(chatHistory, input);

            return chatHistory;
        }

        // =========================================================
        // 4. Add system instructions
        // =========================================================

        private static void AddSystemMessage(ChatHistory chatHistory)
        {
            chatHistory.AddSystemMessage(AnomalyExplanationPrompt.SystemInstructions);
        }

        // =========================================================
        // 5. Add anomaly prompt
        // =========================================================

        private static void AddUserMessage(ChatHistory chatHistory, AnomalyExplanationInput input)
        {
            var prompt = AnomalyExplanationPrompt.Build(input);

            chatHistory.AddUserMessage(prompt);
        }

        // =========================================================
        // 6. Call the LLM
        // =========================================================

        private static async Task<ChatMessageContent> GetCompletionAsync(
                IChatCompletionService chatService,
                ChatHistory chatHistory,
                OpenAIPromptExecutionSettings executionSettings,
                CancellationToken cancellationToken)
        {
            var response = await chatService.GetChatMessageContentAsync( chatHistory, executionSettings, cancellationToken: cancellationToken);

            return response;
        }

        // =========================================================
        // 7. Extract response content
        // =========================================================

        private static string ExtractContent(ChatMessageContent response)
        {
            var content = response.Content;

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException(
                    "Anomaly explanation AI returned an empty response.");
            }

            return content.Trim();
        }

        // =========================================================
        // 8. Deserialize structured response
        // =========================================================

        private static AnomalyExplanationResult DeserializeResult(string content)
        {
            try
            {
                var result = JsonSerializer.Deserialize<AnomalyExplanationResult>(content,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                if (result is null)
                {
                    throw new InvalidOperationException(
                        "Anomaly explanation response could not be deserialized.");
                }

                return result;
            }
            catch (JsonException exception)
            {
                throw new InvalidOperationException(
                    "Anomaly explanation AI returned invalid JSON.",
                    exception);
            }
        }

        // =========================================================
        // 9. Validate result
        // =========================================================

        private static void ValidateResult(AnomalyExplanationResult result)
        {
            if (string.IsNullOrWhiteSpace(result.Summary))
            {
                throw new InvalidOperationException(
                    "AI response is missing Summary.");
            }

            if (string.IsNullOrWhiteSpace(result.Explanation))
            {
                throw new InvalidOperationException(
                    "AI response is missing Explanation.");
            }

            if (string.IsNullOrWhiteSpace(result.Confidence))
            {
                throw new InvalidOperationException(
                    "AI response is missing Confidence.");
            }
        }
    }
}
