using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Processing.Application.Pipeline
{
    public sealed class PackageNormalizationStep : IProcessingStep
    {
        public int Order => ProcessingStepOrder.Package;

        public string Name => "PackageNormalization";

        public Task ExecuteAsync( ProcessingContext context,CancellationToken cancellationToken)
        {
            var input = context.Input;

            var quantity = input.Quantity;
            var unit = input.Unit;
            var productName = input.ProductName;

            if (!quantity.HasValue ||
                string.IsNullOrWhiteSpace(unit))
            {
                return Task.CompletedTask;
            }

            var normalizedUnit = NormalizeUnit(unit);

            var normalizedQuantity = NormalizeQuantity( quantity.Value, normalizedUnit);

            var unitsPerPackage =ExtractUnitsPerPackage(productName);

            if (!unitsPerPackage.HasValue)
            {
                unitsPerPackage = 1;
            }

            var totalQuantity = normalizedQuantity * unitsPerPackage.Value;

            context.ProcessedPrice.SetPackageInformation(
                    normalizedQuantity,
                    normalizedUnit,
                    unitsPerPackage.Value,
                    totalQuantity,
                    normalizedUnit);

            return Task.CompletedTask;
        }

        private static string NormalizeUnit(
            string unit)
        {
            return unit.Trim().ToLowerInvariant() switch
            {
                "ml" => "L",
                "milliliter" => "L",
                "milliliters" => "L",

                "l" => "L",
                "liter" => "L",
                "liters" => "L",

                "g" => "KG",
                "gram" => "KG",
                "grams" => "KG",

                "kg" => "KG",
                "kilogram" => "KG",
                "kilograms" => "KG",

                "pcs" => "PCS",
                "pc" => "PCS",
                "piece" => "PCS",
                "pieces" => "PCS",

                _ => throw new InvalidOperationException(
                    $"Unsupported package unit: {unit}")
            };
        }

        private static decimal NormalizeQuantity(decimal quantity, string originalUnit)
        {
            return originalUnit.Trim().ToLowerInvariant() switch
            {
                "ml" =>
                    quantity / 1000m,

                "milliliter" =>
                    quantity / 1000m,

                "milliliters" =>
                    quantity / 1000m,

                "l" =>
                    quantity,

                "liter" =>
                    quantity,

                "liters" =>
                    quantity,

                "g" =>
                    quantity / 1000m,

                "gram" =>
                    quantity / 1000m,

                "grams" =>
                    quantity / 1000m,

                "kg" =>
                    quantity,

                "kilogram" =>
                    quantity,

                "kilograms" =>
                    quantity,

                "pcs" =>
                    quantity,

                "pc" =>
                    quantity,

                "piece" =>
                    quantity,

                "pieces" =>
                    quantity,

                _ => throw new InvalidOperationException(
                    $"Unsupported package unit: {originalUnit}")
            };
        }

        private static decimal? ExtractUnitsPerPackage(
            string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return null;
            }

            var patterns = new[]
            {
            @"(?:pack|case|box|bundle)\s*(?:of|x)?\s*(\d+)",
            @"(?:x|×)\s*(\d+)",
            @"(\d+)\s*(?:pcs|pieces|pack)"
        };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(
                    productName,
                    pattern,
                    RegexOptions.IgnoreCase);

                if (match.Success &&
                    decimal.TryParse(
                        match.Groups[1].Value,
                        out var count))
                {
                    return count;
                }
            }

            return null;
        }
    }
}
