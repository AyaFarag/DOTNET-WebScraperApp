using API.Endpoints;
using API.Scenario.Configurations;
using API.Scenario.Endpoint;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScenarioConfigurations();
builder.Services.AddControllers();


builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(
              builder.Configuration.GetConnectionString(
                  "DefaultConnection"));
});
builder.Services.AddHangfireServer();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapScenarioEndpoints();

app.MapIngestionEndpoints();
app.UseHangfireDashboard("/hangfire");

Infrastructure.Services.BackgroundJobs.OutboxJobRegistration.Register();
Validation.Infrastructure.Presistance.BackgroundJobs.OutboxJobRegistration.Register();
Processing.Infrastructure.Presistance.BackgroundJobs.OutboxJobRegistration.Register();

app.Run();
