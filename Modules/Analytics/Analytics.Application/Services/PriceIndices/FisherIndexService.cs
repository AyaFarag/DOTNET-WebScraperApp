using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.PriceIndices
{
    public sealed class FisherIndexService
        : IFisherIndexService
    {
        private readonly IFisherIndexRepository _repository;
        private readonly ILaspeyresIndexService _laspeyresService;
        private readonly IPaascheIndexService _paascheService;

        public FisherIndexService(
            IFisherIndexRepository repository,
            ILaspeyresIndexService laspeyresService,
            IPaascheIndexService paascheService)
        {
            _repository = repository;
            _laspeyresService = laspeyresService;
            _paascheService = paascheService;
        }

        public async Task<FisherIndexDto?> CalculateAsync(
            string categoryCode,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            if (baseDate >= currentDate)
            {
                throw new ArgumentException(
                    "Base date must be earlier than current date.");
            }

            var existing =
                await _repository.GetAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (existing is not null)
            {
                return MapToDto(existing);
            }

            var laspeyres =
                await _laspeyresService.CalculateAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (laspeyres is null)
                return null;

            var paasche =
                await _paascheService.CalculateAsync(
                    categoryCode,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (paasche is null)
                return null;

            var fisher = new FisherIndex(
                categoryCode,
                currency,
                laspeyres.IndexValue,
                paasche.IndexValue,
                baseDate,
                currentDate);

            await _repository.AddAsync(
                fisher,
                cancellationToken);

            return MapToDto(fisher);
        }

        public async Task<IReadOnlyCollection<FisherIndexDto>>
            GetAsync(
                string? categoryCode,
                string currency,
                DateTime? from,
                DateTime? to,
                CancellationToken cancellationToken)
        {
            var indexes =
                await _repository.GetAsync(
                    categoryCode,
                    currency,
                    from,
                    to,
                    cancellationToken);

            return indexes
                .Select(MapToDto)
                .ToList();
        }

        private static FisherIndexDto MapToDto(
            FisherIndex index)
        {
            return new FisherIndexDto
            {
                CategoryCode = index.CategoryCode,
                Currency = index.Currency,
                LaspeyresValue = index.LaspeyresValue,
                PaascheValue = index.PaascheValue,
                IndexValue = index.IndexValue,
                ChangePercentage = index.ChangePercentage,
                BaseDate = index.BaseDate,
                CurrentDate = index.CurrentDate
            };
        }
    }
}
