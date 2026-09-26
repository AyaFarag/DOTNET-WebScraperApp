using Index.Application.DTOs.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.Interfaces.Search
{
    public interface INaturalLanguageSearchService
    {
        Task<SearchResultDto> SearchAsync(string query,CancellationToken cancellationToken);
    }
}
