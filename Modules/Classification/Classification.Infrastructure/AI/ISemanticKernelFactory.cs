using Microsoft.SemanticKernel;

namespace Classification.Infrastructure.AI
{
    public interface ISemanticKernelFactory
    {
        Kernel CreateKernel();
    }
}
