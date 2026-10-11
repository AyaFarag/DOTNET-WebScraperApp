using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using API.Endpoints;
using API.Scenario.Configurations;
using API.Scenario.Endpoint;
using Asp.Versioning;
using Classification.Application.Configuration;
using Classification.Infrastructure.Configuration;
using Hangfire;
using Index.Application.Interfaces.Search;
using Ingestion.Application.Configurations;
using Ingestion.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Processing.Application.Configuration;
using Processing.Infrastructure.Configuration;
using Source.Application.Configuration;
using Source.Infrastructure.Configuration;
using Validation.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScenarioConfigurations();
builder.Services.AddControllers();

builder.Services.AddSourceApplication();
builder.Services.AddSourceInfrastructure(builder.Configuration);

builder.Services.AddIngestionApplication();
builder.Services.AddIngestionInfrastructure(builder.Configuration);

builder.Services.AddValidationInfrastructure(builder.Configuration);

builder.Services.AddProcessingApplication();
builder.Services.AddProcessingInfrastructure(builder.Configuration);

builder.Services.AddClassificationApplication();
builder.Services.AddClassificationInfrastructure(builder.Configuration);



builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(
              builder.Configuration.GetConnectionString(
                  "DefaultConnection"));
});
builder.Services.AddHangfireServer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader =
    new UrlSegmentApiVersionReader();
});




builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
using var scope = app.Services.CreateScope();

var analyticsSearchInitializer =
    scope.ServiceProvider
        .GetRequiredService<
            IPriceSnapshotSearchIndexInitializer>();

await analyticsSearchInitializer.InitializeAsync(CancellationToken.None);



var indexSearchInitializer = scope.ServiceProvider
        .GetRequiredService<
            IAzureSearchIndexInitializer>();

await indexSearchInitializer.InitializeAsync(CancellationToken.None);



app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//var versionSet = app.NewApiVersionSet()
//    .HasApiVersion(new ApiVersion(1, 0))
//    .HasApiVersion(new ApiVersion(2, 0))
//    .ReportApiVersions()
//    .Build();
app.MapScenarioEndpoints();

app.MapSourceEndpoints();
app.MapIngestionEndpoints();
// mapValidationEndpoints
// mapProcessingEndpoints
app.MapClassificationEndpoints();
app.MapPriceStatisticsEndpoints(); // extends IEndpointRouteBuilder instead od WebApplication and lives in API/Endpoints/AnalyticsEndpoints.cs


app.UseHangfireDashboard("/hangfire");

Infrastructure.Services.BackgroundJobs.OutboxJobRegistration.Register();
Validation.Infrastructure.Presistance.BackgroundJobs.OutboxJobRegistration.Register();
Processing.Infrastructure.Presistance.BackgroundJobs.OutboxJobRegistration.Register();
Classification.Infrastructure.Repositories.BackgroundJobs.OutboxRepository.Register();
Index.Infrastructure.Presistance.Repositories.BackgroundJobs.OutboxRepository.Register();
Analytics.Infrastructure.Persistence.Service.BackgroundJobs.OutboxJobRegistration.Register();

// Enable CORS for the /sources endpoint
app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:4200") // Replace with your frontend URL
          .AllowAnyHeader()
          .AllowAnyMethod();
});
app.Run();
