using Analytics.Application.DTOs.AI.Search;
using Azure.Search.Documents.Indexes.Models;

namespace Analytics.Infrastructure.Persistence.Repositories.AI.Service
{
    public static class PriceSnapshotSearchIndex
    {
        public static SearchIndex Create(
            string indexName)
        {
            var fields =
                new List<SearchField>
                {
                new(
                    nameof(PriceSnapshotSearchDocument.Id),
                    SearchFieldDataType.String)
                {
                    IsKey = true,
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.ProductKey),
                    SearchFieldDataType.String)
                {
                    IsSearchable = true,
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.ProductName),
                    SearchFieldDataType.String)
                {
                    IsSearchable = true,
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.Brand),
                    SearchFieldDataType.String)
                {
                    IsSearchable = true,
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.CategoryCode),
                    SearchFieldDataType.String)
                {
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.Source),
                    SearchFieldDataType.String)
                {
                    IsSearchable = true,
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.Price),
                    SearchFieldDataType.Double)
                {
                    IsFilterable = true,
                    IsSortable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.UnitPrice),
                    SearchFieldDataType.Double)
                {
                    IsFilterable = true,
                    IsSortable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.Currency),
                    SearchFieldDataType.String)
                {
                    IsFilterable = true
                },

                new(
                    nameof(PriceSnapshotSearchDocument.CollectedAt),
                    SearchFieldDataType.DateTimeOffset)
                {
                    IsFilterable = true,
                    IsSortable = true
                }
                };

            return new SearchIndex(indexName,fields);
        }
    }
}
