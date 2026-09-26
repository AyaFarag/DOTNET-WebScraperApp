using Classification.Application.DTOs;
using Classification.Application.Interfaces.Agents;
using Classification.Infrastructure.AI.Plugins;
using Classification.Infrastructure.AI.Prompts;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;



namespace Classification.Infrastructure.AI.Agents
{

    public sealed class ProductClassificationAgent : IProductClassificationAgent
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly TaxonomyPlugin _taxonomyPlugin;

        public ProductClassificationAgent(IChatCompletionService chatCompletionService, TaxonomyPlugin taxonomyPlugin)
        {
            _chatCompletionService = chatCompletionService;
            _taxonomyPlugin = taxonomyPlugin;
        }

        public async Task<AgentExecutionResult<ProductClassificationResult>> ClassifyAsync(ProcessedProductInput product,CancellationToken cancellationToken)
        {
            // 1. Get taxonomy
            var taxonomy = await _taxonomyPlugin.GetTaxonomyAsync(cancellationToken);

            // 2. Serialize product
            var productJson = JsonSerializer.Serialize(product);

            // 3. Build prompt
            var prompt = $$"""
            You are an enterprise product
            classification agent.

            Your task is to classify a retail product
            using ONLY the approved taxonomy.

            APPROVED TAXONOMY:
            {{taxonomy}}

            PRODUCT:
            {{productJson}}

            Classification rules:

            1. Never invent taxonomy codes.
            2. Use the most specific valid category.
            3. Return confidence between 0 and 1.
            4. Explain the classification briefly.
            5. Return ONLY valid JSON.

            Required JSON format:

            {
              "categoryCode": "...",
              "subCategoryCode": "...",
              "productGroupCode": "...",
              "productTypeCode": "...",
              "confidence": 0.0,
              "reason": "..."
            }
            """;

            // Structured output settings
            var executionSettings =
                new OpenAIPromptExecutionSettings
                {
                    ResponseFormat =
                        typeof(AgentExecutionResult<ProductClassificationResult>),

                    Temperature = 0
                };

            var arguments = new KernelArguments(executionSettings);

            // 4. Create chat history
            var chatHistory = new ChatHistory();

            chatHistory.AddSystemMessage(ProductClassificationPrompt.Instructions);

            chatHistory.AddUserMessage(prompt);

            // 5. Ask the LLM
            var response =  await _chatCompletionService.GetChatMessageContentAsync(chatHistory,cancellationToken:cancellationToken);

            // 6. Extract response
            var responseText = response.Content;

            if (string.IsNullOrWhiteSpace(responseText))
            {
                throw new InvalidOperationException(
                    "Classification agent returned " +
                    "an empty response.");
            }

            // 7. Deserialize structured result
            var result = JsonSerializer.Deserialize<AgentExecutionResult<ProductClassificationResult>>(responseText,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

            if (result is null)
            {
                throw new InvalidOperationException(
                    "Could not deserialize AI " +
                    "classification result.");
            }

            // 8. Validate basic AI output
            ValidateResult(result);

            return result;
        }

        private static void ValidateResult(AgentExecutionResult<ProductClassificationResult> result)
        {
            if (string.IsNullOrWhiteSpace(
                    result.Result.CategoryCode))
            {
                throw new InvalidOperationException(
                    "AI returned an empty category code.");
            }

            if (result.Result.Confidence < 0 ||
                result.Result.Confidence > 1)
            {
                throw new InvalidOperationException(
                    "AI returned invalid confidence.");
            }
        }



    }
}
