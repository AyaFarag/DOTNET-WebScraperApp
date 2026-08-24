using Shared.Domain;
using Validation.Application.DTOs;

namespace Validation.Application.Interfaces.Services
{
    public interface IValidationRule
    {
        string Name { get; }

        Task<ValidationRuleResult> ValidateAsync(RawPrice price,CancellationToken cancellationToken = default);
    }
}
