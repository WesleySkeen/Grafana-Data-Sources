using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);
var port = 5000;
var metricScraperUrl = $"http://localhost:{port}/metrics";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(port);
});

var rb = ResourceBuilder.CreateDefault().AddService("weather-forecast-api",
    serviceVersion: "1.0.0.0", serviceInstanceId: Environment.MachineName);
builder.Services.AddOpenTelemetryMetrics(options =>
{
    options.SetResourceBuilder(rb)
        .AddRuntimeInstrumentation()
        .AddHttpClientInstrumentation();
    
    options.AddPrometheusExporter();
});	

var app = builder.Build();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();