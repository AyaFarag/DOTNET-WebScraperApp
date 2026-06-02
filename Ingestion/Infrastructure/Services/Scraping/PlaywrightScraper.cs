using DocumentFormat.OpenXml.Wordprocessing;
using Ingestion.Application.Configurations;
using Ingestion.Application.DTOs;
using Ingestion.Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services.Scraping
{
    public class PlaywrightScraper : IScraper
    {
        private readonly ScraperOptions _options;
        public PlaywrightScraper(IOptionsSnapshot<ScraperOptions> options)
        {
            _options = options.Value;
        }

        // Dynamic Pages
        // Pagination
        // Anti-Bot Reality

        #region Playwright Scraping Logic (Commented Out)
        //public async Task<List<RawPriceDto>> ScrapeAsync()
        //{
        //    var results = new List<RawPriceDto>();

        //    using var playwright = await Playwright.CreateAsync();

        //    var browser = await playwright.Chromium.LaunchAsync(
        //        new BrowserTypeLaunchOptions
        //        {
        //            Headless = _options.Headless,
        //            Timeout = _options.Timeout
        //        });
        //    var context = await browser.NewContextAsync();
        //    await LoginAsync(context);
        //    foreach (var source in _options.Sources.Where(s => s.IsEnabled))
        //    {
        //        IPage? page = null;

        //        try
        //        {
        //            page = await browser.NewPageAsync();

        //            // Important: set per-page timeout
        //            page.SetDefaultTimeout(_options.Timeout);
        //            page.SetDefaultNavigationTimeout(_options.Timeout);

        //            await page.GotoAsync(source.Url, new PageGotoOptions
        //            {
        //                WaitUntil = WaitUntilState.NetworkIdle
        //            });

        //            // Ensure DOM is ready
        //            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        //            // Wait for product container
        //            await page.WaitForSelectorAsync(source.ProductSelector);

        //            var productElements = await page.QuerySelectorAllAsync(source.ProductSelector);

        //            if (productElements == null || productElements.Count == 0)
        //                continue;

        //            foreach (var productEl in productElements)
        //            {
        //                try
        //                {
        //                    if (productEl == null) continue;

        //                    var name = (await productEl.TextContentAsync())?.Trim();

        //                    var priceEl = await productEl.QuerySelectorAsync(source.PriceSelector);

        //                    var price = priceEl != null
        //                        ? (await priceEl.TextContentAsync())?.Trim()
        //                        : null;

        //                    if (string.IsNullOrWhiteSpace(name) ||
        //                        string.IsNullOrWhiteSpace(price))
        //                        continue;

        //                    results.Add(new RawPriceDto
        //                    {
        //                        ProductName = name,
        //                        RawPrice = price,
        //                        Source = source.Name,
        //                        CollectedAt = DateTime.UtcNow
        //                    });
        //                }
        //                catch
        //                {
        //                    // Skip broken product item, continue pipeline
        //                    continue;
        //                }
        //            }

        //            // polite delay (anti-blocking pattern)
        //            await page.WaitForTimeoutAsync(_options.Timeout);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Source-level failure should NOT break pipeline
        //            Console.WriteLine($"[SCRAPER ERROR] Source: {source.Name}, Error: {ex.Message}");
        //        }
        //        finally
        //        {
        //            if (page != null)
        //                await page.CloseAsync();
        //        }
        //    }

        //    await browser.CloseAsync();

        //    return results;
        //}

        #endregion

        //public async Task<List<RawPriceDto>> ScrapeAsync()
        //{
        //    var results = new List<RawPriceDto>();

        //    using var playwright = await Playwright.CreateAsync();


        //    var browser = await playwright.Chromium.LaunchAsync(
        //            new BrowserTypeLaunchOptions
        //            {
        //                Headless = false,
        //                SlowMo = 500
        //            });
        //    // Shared browser context
        //    var context = await browser.NewContextAsync();

        //    // -----------------------------
        //    // LOGIN
        //    // -----------------------------
        //    await LoginAsync(context);

        //    foreach (var source in _options.Sources.Where(s => s.IsEnabled))
        //    {
        //        //if (source.Url == "https://hostinger.titan.email/mail") 
        //        //{

        //        //}
        //        IPage? page = null;

        //        try
        //        {
        //            page = await context.NewPageAsync();

        //            page.SetDefaultTimeout(_options.Timeout);
        //            page.SetDefaultNavigationTimeout(_options.Timeout);

        //            await page.GotoAsync(
        //                source.Url,
        //                new PageGotoOptions
        //                {
        //                    WaitUntil = WaitUntilState.DOMContentLoaded,
        //                });

        //            await page.WaitForLoadStateAsync(
        //                LoadState.DOMContentLoaded);

        //            // Wait for products container
        //            await page.WaitForSelectorAsync(
        //                source.ProductSelector);

        //            await ScrollThreadListAsync(page);

        //            await page.WaitForTimeoutAsync(3000);

        //            var productElements =
        //                await page.QuerySelectorAllAsync(
        //                    source.ProductSelector);

        //            if (productElements == null ||
        //                productElements.Count == 0)
        //            {
        //                continue;
        //            }

        //            foreach (var productEl in productElements)
        //            {
        //                try
        //                {
        //                    var name =
        //                        (await productEl.TextContentAsync())
        //                        ?.Trim();
        //                    var email = await productEl.GetAttributeAsync("data-email");

        //                    //var priceEl =
        //                    //    await productEl.QuerySelectorAsync(
        //                    //        source.PriceSelector);

        //                    //var price =
        //                    //    priceEl != null
        //                    //    ? (await priceEl.TextContentAsync())
        //                    //        ?.Trim()
        //                    //    : null;

        //                    //if (string.IsNullOrWhiteSpace(name) ||
        //                    //    string.IsNullOrWhiteSpace(price))
        //                    //{
        //                    //    continue;
        //                    //}

        //                    results.Add(new RawPriceDto
        //                    {
        //                       // ProductName = name,
        //                        Email = email,
        //                       // RawPrice = price,
        //                       // Source = source.Name,
        //                       // CollectedAt = DateTime.UtcNow
        //                    });
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(
        //                        $"[PRODUCT ERROR] {ex.Message}");

        //                    continue;
        //                }
        //            }

        //            // polite delay
        //            await page.WaitForTimeoutAsync(_options.Timeout);
        //            Console.WriteLine(productElements.Count);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(
        //                $"[SCRAPER ERROR] Source: {source.Name}, Error: {ex.Message}");
        //        }
        //        finally
        //        {
        //            if (page != null)
        //            {
        //                await page.CloseAsync();
        //            }
        //        }
        //    }

        //    await browser.CloseAsync();

        //    return results;
        //}


        public async Task<HashSet<string>> ScrapeAsync()
        {
            var results = new List<RawPriceDto>();
            var uniqueEmails = new HashSet<string>();
            using var playwright = await Playwright.CreateAsync(); 
            
            await using var browser = 
                await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions 
                    { 
                        Headless = false, 
                        SlowMo = 300 
                    }); 
            var context = await browser.NewContextAsync(); 
            await LoginAsync(context); 

            foreach (var source in _options.Sources.Where(s => s.IsEnabled))
            {
                IPage? page = null; try
                {
                    page = await context.NewPageAsync(); 
                    page.SetDefaultTimeout(_options.Timeout); 
                    await page.GotoAsync(source.Url, 
                        new PageGotoOptions 
                        { 
                            WaitUntil = WaitUntilState.DOMContentLoaded
                        }); 
                    await page.WaitForSelectorAsync(source.ProductSelector); 
                  
                    // CORRECT THREAD CONTAINER
                    var container = page.Locator( ".thread-list .list-container .scroll-region"); 
                    await container.HoverAsync(); 
                
                    
                    string? lastEmail = null; 
                    
                    int noChangeRounds = 0; 
                    while (noChangeRounds < 5) 
                        // important: allow more cycles
                        { var elements = await page.QuerySelectorAllAsync( source.ProductSelector); 
                        
                        int beforeCount = uniqueEmails.Count; 
                        
                        foreach (var el in elements) 
                        { 
                            var email = await el.GetAttributeAsync("data-email"); 
                            var name = (await el.TextContentAsync())?.Trim();

                             
                            if (string.IsNullOrWhiteSpace(email)) 
                                continue;
                            //if (email == "marketing@crisecure.com")
                            //{
                            //    break;
                            //}

                            uniqueEmails.Add(email); 
                            results.Add(new RawPriceDto 
                            { 
                                Email = email, 
                              //  ProductName = name 
                            }); } 
                        
                        var currentLastEmail = 
                            elements.Count > 0 ? 
                            await elements.Last().GetAttributeAsync("data-email") : null; 
                        
                        Console.WriteLine($"Last email: {currentLastEmail}"); // SCROLL AGAIN
                          
                        await page.Mouse.WheelAsync(0, 3000); 
                        await page.WaitForTimeoutAsync(2000); // CHECK IF NEW DATA ARRIVED
                        
                        if (uniqueEmails.Count == beforeCount) 
                        { 
                            noChangeRounds++; 
                        } 
                        else { 
                            noChangeRounds = 0; 
                        } 
                        lastEmail = currentLastEmail; 

                        Console.WriteLine($"Unique emails collected: {uniqueEmails.Count}");
                    } 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine( $"[SCRAPER ERROR] {ex.Message}"); 
                } finally
                { 
                    if (page != null) 
                    { 
                        await page.CloseAsync(); 
                    } 
                } 
            } 
            await browser.CloseAsync(); 
           
            return uniqueEmails; 
        }


        private async Task LoginAsync(IBrowserContext context)
        {
            var page = await context.NewPageAsync();

            try
            {
                page.SetDefaultTimeout(_options.Timeout);

                await page.GotoAsync(
                    "https://hostinger.titan.email/login/",
                    new PageGotoOptions
                    {
                        WaitUntil = WaitUntilState.DOMContentLoaded,
                        Timeout = 60000
                    });

                // EMAIL
                await page.FillAsync(
                    "input[name='email']",
                    _options.Sources[1].Email);

                // PASSWORD
                await page.FillAsync(
                    "input[name='password']",
                    _options.Sources[1].Password);

                // LOGIN BUTTON
                await page.ClickAsync(
                    "button[type='button']");

                // Wait inbox/dashboard
                await page.WaitForLoadStateAsync(
                    LoadState.DOMContentLoaded);

                // Optional small delay
                await page.WaitForTimeoutAsync(5000);

                Console.WriteLine("[LOGIN SUCCESS]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[LOGIN ERROR] {ex.Message}");

                throw;
            }
            finally
            {
                await page.CloseAsync();
            }
        }

        private async Task ScrollThreadListAsync(IPage page)
        {
            var container = page.Locator(".scroll-region");

            await container.WaitForAsync();

            int previousScrollTop = -1;

            while (true)
            {
                // current scroll position
                var currentScrollTop =
                    await container.EvaluateAsync<int>(
                        "el => el.scrollTop");

                // scroll down
                await container.EvaluateAsync(
                    "(el) => el.scrollBy(0, 1500)");

                await page.WaitForTimeoutAsync(1500);

                // new scroll position
                var newScrollTop =
                    await container.EvaluateAsync<int>(
                        "el => el.scrollTop");

                // no more scrolling possible
                if (newScrollTop == previousScrollTop)
                    break;

                previousScrollTop = newScrollTop;
            }
        }
    }
}
