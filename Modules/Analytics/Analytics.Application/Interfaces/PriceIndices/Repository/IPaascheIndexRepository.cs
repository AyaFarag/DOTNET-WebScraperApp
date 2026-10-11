using Analytics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Application.Interfaces.PriceIndices.Repository
{
    public interface IPaascheIndexRepository
    {
        Task<PaascheIndex?> GetAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PaascheIndex>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken);

        Task AddAsync(
            PaascheIndex index,
            CancellationToken cancellationToken);
    }
}
