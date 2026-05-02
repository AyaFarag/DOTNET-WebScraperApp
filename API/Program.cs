using API.Scenario.Configurations;
using API.Scenario.Endpoint;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScenarioConfigurations();
builder.Services.AddControllers();

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

app.Run();
