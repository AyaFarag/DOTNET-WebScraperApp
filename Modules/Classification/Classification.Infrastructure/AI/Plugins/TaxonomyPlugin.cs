using Classification.Application.Interfaces.Repository;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text.Json;

namespace Classification.Infrastructure.AI.Plugins
{
    public sealed class TaxonomyPlugin
    {
        private readonly ITaxonomyRepository _repository;

        public TaxonomyPlugin(ITaxonomyRepository repository)
        {
            _repository = repository;
        }

        [KernelFunction]
        [Description("Returns the active product classification taxonomy.")]
        public async Task<string> GetTaxonomyAsync(CancellationToken cancellationToken)
        {
            var categories = await _repository.GetActiveCategoriesAsync(cancellationToken);

            return JsonSerializer.Serialize(categories.Select(x => new
                {
                    x.Code,
                    x.Name,
                    x.ParentId,
                    x.Level
                }));
        }
    }
}
