using Classification.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

namespace Classification.Infrastructure.AI
{
    public sealed class SemanticKernelFactory
    : ISemanticKernelFactory
    {
        private readonly AIOptions _options;

        public SemanticKernelFactory(IOptions<AIOptions> options)
        {
            _options = options.Value;
        }

        public Kernel CreateKernel()
        {
            var builder = Kernel.CreateBuilder();

            builder.AddOpenAIChatCompletion(
                modelId: _options.Model,
                apiKey: _options.ApiKey);

            return builder.Build();
        }
    }
}
