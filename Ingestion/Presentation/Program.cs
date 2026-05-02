using Ingestion.Application.Configurations;
using Ingestion.Application.Interfaces;
using Ingestion.Application.Services;
using Ingestion.Infrastructure.Configurations;  
using Ingestion.Infrastructure.Scraping;
using Ingestion.Presentation.Endpoints; 

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<ScraperOptions>(
    builder.Configuration.GetSection("Scraper"));

builder.Services.AddSwaggerGen();
builder.Services.AddIngestionApplication();
builder.Services.AddIngestionInfrastructure();

var app = builder.Build();
//app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI();
app.MapIngestionEndpoints();

app.Run();
