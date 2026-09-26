using Analytics.Application.Interfaces.Repositories;
using Analytics.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Infrastructure.Persistence.Service.Uow
{
    public class AnalyticsUnitOfWork : IAnalyticsUnitOfWork
    {
        private readonly AnalyticsDbContext _context;
        public AnalyticsUnitOfWork(AnalyticsDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
