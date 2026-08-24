using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;
using Processing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Application.Pipeline
{
    public class ProductKeyGenerationStep : IProcessingStep
    {

        //Product key input
        //Brand
        //NormalizedProductName
        //PackageQuantity
        //PackageUnit
        //UnitsPerPackage

        public string Name => "ProductKeyGeneration";

        public int Order => ProcessingStepOrder.ProductKey;


        public Task ExecuteAsync(ProcessingContext context, CancellationToken cancellationToken)
        {
            var price = context.ProcessedPrice;

            var keySource =  BuildKeySource(price);

            var productKey = GenerateSha256(keySource);

            price.SetProductKey(productKey);

            return Task.CompletedTask;
        }

        private static string BuildKeySource(ProcessedPrice price)
        {
            var brand = NormalizeForKey(price.Brand);

            var productName = NormalizeForKey(
                    price.NormalizedProductName);

            var packageQuantity = price.PackageQuantity?.
                ToString("0.######",System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty;

            var packageUnit = NormalizeForKey( price.PackageUnit);

            var unitsPerPackage = price.UnitsPerPackage?.
                ToString( "0.######",System.Globalization.CultureInfo.InvariantCulture)
                ?? string.Empty;

            return string.Join(
                "|",
                brand,
                productName,
                packageQuantity,
                packageUnit,
                unitsPerPackage);
        }

        private static string NormalizeForKey(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return string.Join(" ",value.Trim().ToUpperInvariant().Split(' ',StringSplitOptions. RemoveEmptyEntries))
                .Replace("-", "")
                .Replace("_", "");
        }

        private static string GenerateSha256( string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            var hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }
}
