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
    public async Task<string> Get(string data)
    {
        using var activity = SharedTelemetryUtilities.Writer.StartActivity("get_weather_forecasts");
        
        SharedTelemetryUtilities.RequestCounter.Add(1);
        
        _logger.LogInformation($"my data is {data}"); 
        
        return data;
    }
}