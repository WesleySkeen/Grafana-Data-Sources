using System.Net;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace WeatherForecast.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly HttpClient _httpClient = new();

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _httpClient.BaseAddress = new Uri("https://api.open-meteo.com");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        using var activity = SharedTelemetryUtilities.Writer.StartActivity("get_weather_forecasts");
        
        SharedTelemetryUtilities.RequestCounter.Add(1);
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
        using (LogContext.PushProperty("app", "api"))
        {
            _logger.LogInformation("Help my CC # is 4567-6543-4567-8777");    
        }
        // _logger.LogInformation("Max Celsius temperature was {temp}", forecasts.Max(x => x.TemperatureC));
        
        // External calls to demonstrate tracing
        // await _httpClient.GetAsync("/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m");
        // await _httpClient.GetAsync("/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m");

        return forecasts.ToArray();
    }
}