using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceIndices.Repository;
using Analytics.Application.Interfaces.PriceIndices.Service;
using Analytics.Application.Interfaces.Repositories;
using Analytics.Domain.Entities;

namespace Analytics.Application.Services.PriceIndices
{

    public sealed class PriceRelativeService : IPriceRelativeService
    {
        private readonly IPriceSnapshotRepository _snapshotRepository;
        private readonly IPriceRelativeRepository _relativeRepository;
        private readonly IAnalyticsUnitOfWork _unitOfWork;

        public PriceRelativeService(
            IPriceSnapshotRepository snapshotRepository,
            IPriceRelativeRepository relativeRepository,
            IAnalyticsUnitOfWork unitOfWork)
        {
            _snapshotRepository = snapshotRepository;
            _relativeRepository = relativeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PriceRelativeDto?> CalculateAsync(
            string productKey,
            string? source,
            string currency,
            DateTime baseDate,
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            var snapshots =
                await _snapshotRepository.GetProductHistoryAsync(
                    productKey,
                    baseDate,
                    currentDate,
                    source,
                    currency,
                    cancellationToken);

            if (snapshots.Count == 0)
                return null;

            var baseSnapshot =
                snapshots
                    .Where(x => x.CollectedAt <= baseDate)
                    .OrderByDescending(x => x.CollectedAt)
                    .FirstOrDefault();

            var currentSnapshot =
                snapshots
                    .Where(x => x.CollectedAt <= currentDate)
                    .OrderByDescending(x => x.CollectedAt)
                    .FirstOrDefault();

            if (baseSnapshot is null ||
                currentSnapshot is null)
            {
                return null;
            }

            var existing =
                await _relativeRepository.GetAsync(
                    productKey,
                    source,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            if (existing is not null)
                return Map(existing);

            var relative =
                new PriceRelative(
                    baseSnapshot.ProductKey,
                    baseSnapshot.ProductName,
                    baseSnapshot.Brand,
                    baseSnapshot.CategoryCode,
                    baseSnapshot.Source,
                    baseSnapshot.Currency,
                    baseSnapshot.Price,
                    currentSnapshot.Price,
                    baseSnapshot.CollectedAt,
                    currentSnapshot.CollectedAt);

            await _relativeRepository.AddAsync(
                relative,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Map(relative);
        }

        public async Task<IReadOnlyCollection<PriceRelativeDto>>
            CalculateAllAsync(
                string? categoryCode,
                string? source,
                string currency,
                DateTime baseDate,
                DateTime currentDate,
                CancellationToken cancellationToken)
        {
            var snapshots =
                await _snapshotRepository.GetAsync(
                    categoryCode,
                    source,
                    currency,
                    baseDate,
                    currentDate,
                    cancellationToken);

            var results =
                new List<PriceRelativeDto>();

            var groups =
                snapshots.GroupBy(
                    x => new
                    {
                        x.ProductKey,
                        x.Source,
                        x.Currency
                    });

            foreach (var group in groups)
            {
                var baseSnapshot =
                    group
                        .Where(x => x.CollectedAt <= baseDate)
                        .OrderByDescending(x => x.CollectedAt)
                        .FirstOrDefault();

                var currentSnapshot =
                    group
                        .Where(x => x.CollectedAt <= currentDate)
                        .OrderByDescending(x => x.CollectedAt)
                        .FirstOrDefault();

                if (baseSnapshot is null ||
                    currentSnapshot is null)
                {
                    continue;
                }

                var relative = new PriceRelative(
                        baseSnapshot.ProductKey,
                        baseSnapshot.ProductName,
                        baseSnapshot.Brand,
                        baseSnapshot.CategoryCode,
                        baseSnapshot.Source,
                        baseSnapshot.Currency,
                        baseSnapshot.Price,
                        currentSnapshot.Price,
                        baseSnapshot.CollectedAt,
                        currentSnapshot.CollectedAt);

                await _relativeRepository.AddAsync(
                    relative,
                    cancellationToken);

                results.Add(Map(relative));
            }

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return results;
        }

        private static PriceRelativeDto Map(PriceRelative entity)
        {
            return new PriceRelativeDto
            {
                ProductKey = entity.ProductKey,
                ProductName = entity.ProductName,
                Brand = entity.Brand,
                CategoryCode = entity.CategoryCode,
                Source = entity.Source,
                Currency = entity.Currency,
                BasePrice = entity.BasePrice,
                CurrentPrice = entity.CurrentPrice,
                RelativeValue = entity.RelativeValue,
                PriceChangePercentage =
                    entity.RelativeValue - 100m,
                BaseDate = entity.BaseDate,
                CurrentDate = entity.CurrentDate
            };
        }
    }
}
