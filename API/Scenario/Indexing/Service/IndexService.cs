using API.Scenario.Indexing.DTO;
using API.Scenario.Indexing.Interface;
using API.Scenario.Persistence;

namespace API.Scenario.Indexing.Service
{
    public class IndexService : IIndexService
    {
        private readonly AppDbContext _db;

        public IndexService(AppDbContext db)
        {
            _db = db;
        }

        public Task<CPIResult> Calculate()
        {
            var prices = _db.Prices.ToList();

            // Simple average (replace with real formula)
            var avg = prices.Average(x => x.Price);

            return Task.FromResult(new CPIResult { Value = avg });
        }
    }
}
