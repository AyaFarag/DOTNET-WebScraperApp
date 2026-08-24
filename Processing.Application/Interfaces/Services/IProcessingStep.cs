using Processing.Application.DTOs;

namespace Processing.Application.Interfaces.Services
{
    public interface IProcessingStep
    {
        int Order { get; }
        string Name { get; }

        Task ExecuteAsync(ProcessingContext context,CancellationToken cancellationToken);
    }
}
