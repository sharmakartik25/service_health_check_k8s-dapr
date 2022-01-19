using DaprHeathCheck.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace DaprHeathCheck.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IHealthz _healthcheck;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHealthz healthcheck)
        {
            _logger = logger;
            this._healthcheck = healthcheck;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var timer = new Stopwatch();

            timer.Start();

            var result = (this._healthcheck.HealthCheck().Result as ObjectResult);

            while (!Enumerable.Range(200, 400).Contains((int)result.StatusCode))
            {
                _logger.LogInformation("Service health not ready, waiting for 1 sec.");
                Thread.Sleep(1000);


                if (timer.ElapsedMilliseconds > 20000)
                {
                    _logger.LogError("Service health still not ready, waited for 20 secs. Now exiting.");

                    throw new Exception("Serice unhealthy. Dependencies not up.");
                }
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}