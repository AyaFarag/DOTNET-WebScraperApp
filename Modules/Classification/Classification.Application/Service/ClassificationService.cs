using Classification.Application.DTOs;
using Classification.Application.Interfaces;
using Classification.Application.Interfaces.Agents;
using Classification.Application.Interfaces.Repository;
using Classification.Application.Interfaces.Service;
using Classification.Domain.Enttities;
using Shared.Contracts.Agents;
using Shared.Contracts.Queries.Processing;
using AgentExecutionLog = Classification.Domain.Enttities.AgentExecutionLog;

namespace Classification.Application.Service
{
    public sealed class ClassificationService : IClassificationService
    {
        private readonly IProcessedProductReader<ProcessedProductInput> _productReader;
        private readonly IProductClassificationAgent _agent;
        private readonly ITaxonomyValidator _taxonomyValidator;
        private readonly IClassificationRepository _classificationRepository;
        private readonly IClassificationExecutionRepository _executionRepository;
        private readonly IAgentExecutionLogRepository<AgentExecutionLog> _agentExecutionLogRepository;


        public ClassificationService(
            IProcessedProductReader<ProcessedProductInput> productReader,
            IProductClassificationAgent agent,
            ITaxonomyValidator taxonomyValidator,
            IClassificationRepository classificationRepository,
            IClassificationExecutionRepository executionRepository,
            IAgentExecutionLogRepository<AgentExecutionLog> agentExecutionLogRepository)
        {
            _productReader = productReader;
            _agent = agent;
            _taxonomyValidator = taxonomyValidator;
            _classificationRepository = classificationRepository;
            _executionRepository = executionRepository;
            _agentExecutionLogRepository = agentExecutionLogRepository;

        }

        public async Task ClassifyBatchAsync(Guid batchId,CancellationToken cancellationToken)
        {
            var products = await _productReader.GetByBatchIdAsync(batchId,cancellationToken);

            if (products.Count == 0)
            {
                throw new InvalidOperationException($"No processed products found for batch {batchId}.");
            }

            // 2. Create execution
            var execution = new ClassificationExecution(batchId, products.Count);

            await _executionRepository.AddAsync(execution,cancellationToken);

            try
            {
                foreach (var product in products)
                {

                    try
                    {
                        var aiResult = await _agent.ClassifyAsync(product, cancellationToken);

                        await _taxonomyValidator.ValidateAsync(aiResult, cancellationToken);

                        var classification = new ClassificationResult(
                            batchId,
                            execution.Id,
                            product.ProcessedPriceId,
                            product.ProductKey,

                            aiResult.Result.CategoryCode,
                            aiResult.Result.SubCategoryCode,
                            aiResult.Result.ProductGroupCode,
                            aiResult.Result.ProductTypeCode,

                            aiResult.Result.Confidence,
                            aiResult.Result.Reason,

                            "AI");

                        await _classificationRepository.AddAsync(classification, cancellationToken);


                        var agentLog = new AgentExecutionLog(
                                    batchId,
                                    product.ProcessedPriceId,
                                    "ProductClassificationAgent",
                                    aiResult.Model,
                                    "classification-v1"
                                    // input
                                   );

                        agentLog.Complete(
                            aiResult.LatencyMs,
                            aiResult.InputTokens,
                            aiResult.OutputTokens,
                            aiResult.Result.Confidence
                            // output
                            );
                        await _agentExecutionLogRepository.AddAsync(agentLog, cancellationToken);

                        // Update execution
                        execution.IncrementClassified();
                    }
                    catch (Exception ex)
                    {
                        var agentLog =
                            new AgentExecutionLog(
                                batchId,
                                product.ProcessedPriceId,
                                "ProductClassificationAgent",
                                "gpt-5",
                                "classification-v1");

                        agentLog.Fail(agentLog.LatencyMs, ex.Message);

                        await _agentExecutionLogRepository.AddAsync(agentLog, cancellationToken);
                        
                        // One product failed
                        execution.IncrementFailed();
                    }

                    // 4. Complete execution
                    execution.Complete();

                }

            }
            catch (Exception ex)
            {
                // 6. Entire execution failed
                execution.Fail(ex.Message);

                throw new Exception("Operation failed", ex);
            }

           

        }
    }
}
