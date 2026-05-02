using API.Scenario.Classification;
using API.Scenario.Persistence.Entities;
using MediatR;

namespace API.Scenario.Persistence
{
    public class PersistenceHandler : INotificationHandler<PriceClassifiedEvent>
    {
        private readonly AppDbContext _db;

        public PersistenceHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task Handle(PriceClassifiedEvent notification, CancellationToken ct)
        {
            var entity = new PriceRecord
            {
                ProductName = notification.ProductName,
                Price = notification.Price,
                Category = notification.Category
            };

            _db.Prices.Add(entity);
            await _db.SaveChangesAsync();
        }
    }
}
