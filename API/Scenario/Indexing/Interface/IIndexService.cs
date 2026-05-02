using API.Scenario.Indexing.DTO;

namespace API.Scenario.Indexing.Interface
{
    public interface IIndexService
    {
        Task<CPIResult> Calculate();
    }
}
