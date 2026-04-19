using Application.Configurations;
using Ingestion.Application.Interfaces;
using Ingestion.Application.Services;
using Ingestion.Infrastructure.Scraping;
using Ingestion.Presentation.Endpoints; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IngestionService>();
builder.Services.AddScoped<IScraper, PlaywrightScraper>();

builder.Services.Configure<ScraperOptions>(
    builder.Configuration.GetSection("Scraper"));


var app = builder.Build();
app.MapGet("/", () => "Hello World!");


app.MapIngestionEndpoints();

app.Run();
