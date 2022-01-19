using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeathCheckAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

//// Instead of exception -- return async custom response about the services up / Down for all dependencies.
//// Can add performance also, to make sure response comes within minimum amount of time.
//// Instead of returning 400/500 always return 200 with status of all dependencies. 
//// Healthcheck should always return a response.

//// ApiBehaviorOptions => 

//// AKS call to custom service health endpoint (through Yaml config files).
//// Components check liveliness through health check (keyvaultsecretstore initially maybe for secet store).

//// Background services -- explore how
