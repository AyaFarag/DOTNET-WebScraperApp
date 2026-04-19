using Application.Configurations;
using DocumentFormat.OpenXml.Wordprocessing;
using Ingestion.Application.DTOs;
using Ingestion.Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Infrastructure.Scraping
{
    public class PlaywrightScraper : IScraper
    {
        private readonly ScraperOptions _options;
        public PlaywrightScraper(IOptionsSnapshot<ScraperOptions> options)
        {
            _options = options.Value;
        }
        public async Task<List<RawPriceDto>> ScrapeAsync()
        {
            var results = new List<RawPriceDto>();

            using var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = _options.Headless,
                    Timeout = _options.Timeout
                });

            foreach (var source in _options.Sources.Where(s => s.IsEnabled))
            {
                IPage? page = null;

                try
                {
                    page = await browser.NewPageAsync();

                    // Important: set per-page timeout
                    page.SetDefaultTimeout(_options.Timeout);
                    page.SetDefaultNavigationTimeout(_options.Timeout);

                    await page.GotoAsync(source.Url, new PageGotoOptions
                    {
                        WaitUntil = WaitUntilState.NetworkIdle
                    });

                    // Ensure DOM is ready
                    await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

                    // Wait for product container
                    await page.WaitForSelectorAsync(source.ProductSelector);

                    var productElements = await page.QuerySelectorAllAsync(source.ProductSelector);

                    if (productElements == null || productElements.Count == 0)
                        continue;

                    foreach (var productEl in productElements)
                    {
                        try
                        {
                            if (productEl == null) continue;

                            var name = (await productEl.TextContentAsync())?.Trim();

                            var priceEl = await productEl.QuerySelectorAsync(source.PriceSelector);

                            var price = priceEl != null
                                ? (await priceEl.TextContentAsync())?.Trim()
                                : null;

                            if (string.IsNullOrWhiteSpace(name) ||
                                string.IsNullOrWhiteSpace(price))
                                continue;

                            results.Add(new RawPriceDto
                            {
                                ProductName = name,
                                RawPrice = price,
                                Source = source.Name,
                                CollectedAt = DateTime.UtcNow
                            });
                        }
                        catch
                        {
                            // Skip broken product item, continue pipeline
                            continue;
                        }
                    }

                    // polite delay (anti-blocking pattern)
                    await page.WaitForTimeoutAsync(800);
                }
                catch (Exception ex)
                {
                    // Source-level failure should NOT break pipeline
                    Console.WriteLine($"[SCRAPER ERROR] Source: {source.Name}, Error: {ex.Message}");
                }
                finally
                {
                    if (page != null)
                        await page.CloseAsync();
                }
            }

            await browser.CloseAsync();

            return results;
        }
    }
}
