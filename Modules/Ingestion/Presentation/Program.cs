using DotNetEnv;
using Hangfire;
using Infrastructure.Persistence.Data;
using Infrastructure.Services.BackgroundJobs;
using Ingestion.Application.Configurations;
using Ingestion.Infrastructure.Configurations;  
using Ingestion.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables();

//builder.Services.Configure<JwtSettings>(
//    builder.Configuration.GetSection("JwtSettings"));

//var connectionString =
//    builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.Configure<ScraperOptions>(
    builder.Configuration.GetSection("Scraper"));

builder.Services.AddSwaggerGen();
builder.Services.AddIngestionApplication();
builder.Services.AddIngestionInfrastructure(builder.Configuration);

builder.Services.AddDbContext<IngestionDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(
              builder.Configuration.GetConnectionString(
                  "DefaultConnection"));
});

builder.Services.AddHangfireServer();

var app = builder.Build();
//app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI();
app.MapIngestionEndpoints();

app.UseHangfireDashboard("/hangfire");
OutboxJobRegistration.Register();
app.Run();
